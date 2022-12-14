using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform to;
    public int speed=20;
    public string side;
    private bool done = false;
    void Update()
    {
        TurnToDestination();
    }
    void TurnToDestination()
    {
        if(transform.rotation.eulerAngles.y < 150f && side == "left")
        {
            done = true;
        }
        else if(transform.rotation.eulerAngles.y < 90f && transform.rotation.eulerAngles.y > 30f && side == "right")
        {
            done = true;
        }

        if(!done)
        {
            Quaternion lookRotation = Quaternion.LookRotation(to.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }
}