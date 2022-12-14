using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    //GameObject child;
    public float noteSpeed = 400;
    public bool hit = false;

    void Update()
    {
        transform.position += Vector3.right * noteSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ARM")
        {
            GameManage.instance.ScoreUp();
            SkinnedMeshRenderer msh = GetComponentInChildren<SkinnedMeshRenderer>();
            msh.enabled = false;
            hit = true;
        }
    }
}
