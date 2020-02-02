using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSlotUI : MonoBehaviour
{
    private Mission mission;
    public Image leftImage;
    public Image rightImage;

    public void Setup(Mission mission, Sprite leftSprite, Sprite rightSprite)
    {
        gameObject.SetActive(true);
        this.mission = mission;
        this.leftImage.sprite = leftSprite;
        this.leftImage.enabled = true;
        this.rightImage.sprite = rightSprite;
        this.rightImage.enabled = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        mission = null;
        this.leftImage.sprite = null;
        this.leftImage.enabled = false;
        this.rightImage.sprite = null;
        this.rightImage.enabled = false;
    }
}
