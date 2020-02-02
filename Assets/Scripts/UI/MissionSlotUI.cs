using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSlotUI : MonoBehaviour
{
    private Mission mission;
    public Image background;
    public Image leftImage;
    public Image rightImage;

    public Animator okAnimator;

    public Mission Mission => mission;

    public void Setup(Mission mission, Sprite leftSprite, Sprite rightSprite)
    {
        this.mission = mission;
        this.background.enabled = true;
        this.leftImage.sprite = leftSprite;
        this.leftImage.enabled = true;
        this.rightImage.sprite = rightSprite;
        this.rightImage.enabled = true;
    }

    public void Hide()
    {
        mission = null;
        this.background.enabled = false;
        this.leftImage.sprite = null;
        this.leftImage.enabled = false;
        this.rightImage.sprite = null;
        this.rightImage.enabled = false;
    }

    public void Fullfill()
    {
        okAnimator.SetTrigger("done");
    }
}
