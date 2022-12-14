using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAnim : MonoBehaviour
{
    public Animator FeverAnimator = null;
    string FeverTime = "FeverTime";
    public UnityEngine.UI.Image FeverImage = null;
    public Sprite judgementSprite = null;

    public void FeverStart()
    {
        gameObject.SetActive(true);
        FeverImage.sprite = judgementSprite;
        FeverAnimator.SetTrigger(FeverTime);
    }

    public void FeverEnd()
    {
        //FeverImage.sprite = null;
        gameObject.SetActive(false);
    }
}
