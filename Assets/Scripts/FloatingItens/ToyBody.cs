using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBody : MonoBehaviour
{
    static int LastInstanceId = 0;

    public bool Protected = true;

    public int InstanceId{get; private set;}

    public int Id { get; private set; }
    public GameObject singleBodyRoot;
    public GameObject fullBodyRoot;
    public Transform[] singleBodyPosition;
    public Transform[] fullBodyPositions;

    public bool[] freeSlots = new bool[] { true, true };
    private Transform[] currentBodyConfiguration;

    private bool isJoined = false;
    private float joinedTimeCount = 0f;

    public List<BodyPart> BodyParts;

    public event Action Joined;
    public event Action Splitted;

    public void Start()
    {
        InstanceId = ++LastInstanceId;
        UpdateBodyPartPositions();
        ProtectAgainstSpamming();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateBodyPartPositions();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Split();
        }
    }

    void ProtectAgainstSpamming()
    {
        StopAllCoroutines();
        StartCoroutine(ProtectAgainstSpammingCR());
    }
    IEnumerator ProtectAgainstSpammingCR()
    {
        Protected = true;
        yield return new WaitForSeconds(1);
        Protected = false;
    }

    public void UpdateBodyPartPositions()
    {
        singleBodyRoot.SetActive(false);
        fullBodyRoot.SetActive(false);
        switch (BodyParts.Count)
        {
            case 0:
                Destroy(gameObject);
                return;
                break;
            case 1:
                currentBodyConfiguration = singleBodyPosition;
                singleBodyRoot.SetActive(true);
                BodyParts[0].TargetPosition = singleBodyPosition[0];
                BodyParts[0].TargetSlot = BodyParts[0].OriginalSlot;

                if (isJoined) TriggerJoinEvent(false);
                break;
            case 2:
                fullBodyRoot.SetActive(true);
                freeSlots[0] = true;
                freeSlots[1] = true;
                for (int i = 0; i < BodyParts.Count; i++)
                {
                    int slot;
                    if (freeSlots[BodyParts[i].OriginalSlot])
                    {
                        slot = BodyParts[i].OriginalSlot;
                    }
                    else
                    {
                        slot = GetFreeSlotIndex();
                    }
                    BodyParts[i].TargetPosition = fullBodyPositions[slot];
                    BodyParts[i].TargetSlot = slot;
                    freeSlots[slot] = false;
                }

                if (!isJoined) TriggerJoinEvent(true);
                break;
        }
    }

    private void TriggerJoinEvent(bool isJoined)
    {
        this.isJoined = isJoined;

        if (isJoined) Joined.SafeInvoke();
        else Splitted.SafeInvoke();
    }

    private int GetFreeSlotIndex()
    {
        for (int i = 0; i < freeSlots.Length; i++)
        {
            if (freeSlots[i])
            {
                return i;
            }
        }
        return 0;
    }

    public void Split()
    {
        if (BodyParts.Count <= 1)
        {
            return;
        }

        BodyPart removedPart = BodyParts[1];
        BodyParts.RemoveAt(1);
        ToyBodyFactory.Instance.InstantiateBody(removedPart, null, transform.position + (Vector3.forward * .5f), transform.rotation);
        UpdateBodyPartPositions();
        ProtectAgainstSpamming();
    }

    public void Join(ToyBody other)
    {
        if (BodyParts.Count > 1 || other.BodyParts.Count > 1)
        {
            return;
        }
        other.BodyParts.Add(BodyParts[0]);
        other.UpdateBodyPartPositions();
        DestroyThis();
    }

    public void DestroyThis()
    {
        ToyBodyFactory.Instance.ToyBodies.Remove(this);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (Protected) return;
        ToyBody other = collision.gameObject.GetComponent<ToyBody>();

        if(other == null) return;
        if(other.InstanceId < InstanceId) return;
        if(other.Protected) return;

        if (this.BodyParts.Count == 1 && other.BodyParts.Count == 1)
        {
            other.Join(this);
            ProtectAgainstSpamming();
        }
        else
        {
            Split();
            other.Split();
        }
    }
}
