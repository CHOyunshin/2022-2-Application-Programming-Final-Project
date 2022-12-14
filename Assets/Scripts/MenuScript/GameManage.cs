using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public int _score = 0;
    public Text tm;
    public static GameManage instance;

    private void Start()
    {
        instance = this;
    }

    public void ScoreUp()
    {
        _score += 100;
        tm.text = "" + _score;
    }
}
