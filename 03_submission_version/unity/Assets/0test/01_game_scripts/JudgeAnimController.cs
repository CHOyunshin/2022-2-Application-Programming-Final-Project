using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeAnimController : MonoBehaviour
{
    public Animator judgementAnimator = null;
    string hit = "Hit";
    public UnityEngine.UI.Image judgementImage = null;
    public Sprite[] judgementSprite = null;


    public void JudgeStart(int p_num)
    {
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(hit);
    }
}
