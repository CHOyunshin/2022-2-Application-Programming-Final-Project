using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openL : MonoBehaviour
{
    public Transform to;
    public int speed=20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TurnToDestination();
    }
    void TurnToDestination()
    {
        Quaternion lookRotation =
            Quaternion.LookRotation(to.transform.position - transform.position);

        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}