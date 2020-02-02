using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyFillingCircle : MonoBehaviour
{
    private ToyJoinReaction toyBodyReaction;
    public GameObject ringParent;
    public Image fillingRing;

    private float height;

    private void Awake()
    {
        toyBodyReaction = GetComponentInParent<ToyJoinReaction>();
        toyBodyReaction.JoinedTimeSpent += UpdateFill;
        toyBodyReaction.Stopped += Deactivate;

        height = transform.position.y - toyBodyReaction.transform.position.y;

        Deactivate();
    }

    private void OnDestroy()
    {
        toyBodyReaction.JoinedTimeSpent -= UpdateFill;
        toyBodyReaction.Stopped -= Deactivate;
    }

    private void LateUpdate()
    {
        transform.position = toyBodyReaction.transform.position + Vector3.up * height;
    }

    private void UpdateFill(float fill)
    {
        ringParent.SetActive(true);
        fillingRing.fillAmount = fill;
    }

    private void Deactivate()
    {
        ringParent.SetActive(false);
    }
}
