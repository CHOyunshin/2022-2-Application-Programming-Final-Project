using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public SocketClient socketClient;
    public GameObject nose;
    public GameObject left_shoulder;
    public GameObject left_elbow;
    public GameObject left_wrist;
    public GameObject right_shoulder;
    public GameObject right_elbow;
    public GameObject right_wrist;
    public GameObject left_hip;
    public GameObject left_knee;
    public GameObject left_ankle;
    public GameObject right_hip;
    public GameObject right_knee;
    public GameObject right_ankle;

    public float gap = 1.3f;
    public float lerpSpeed = 15f;

    void Update()
    {
        JointList jointList = socketClient.jointList;
        if(jointList.joint.Length > 0)
        {
            Joint joint_nose = jointList.joint[0];

            Joint joint_left_shoulder = jointList.joint[11];
            Joint joint_left_elbow = jointList.joint[13];
            Joint joint_left_wrist = jointList.joint[15];

            Joint joint_right_shoulder = jointList.joint[12];
            Joint joint_right_elbow = jointList.joint[14];
            Joint joint_right_wrist = jointList.joint[16];

            Joint joint_left_hip = jointList.joint[23];
            Joint joint_left_knee = jointList.joint[25];
            Joint joint_left_ankle = jointList.joint[27];

            Joint joint_right_hip = jointList.joint[24];
            Joint joint_right_knee = jointList.joint[26];
            Joint joint_right_ankle = jointList.joint[28];


            nose.transform.position = Vector3.Lerp(nose.transform.position, new Vector3(-joint_nose.x, -joint_nose.y, joint_nose.z) * gap, Time.deltaTime*lerpSpeed);
            
            left_shoulder.transform.position = Vector3.Lerp(left_shoulder.transform.position, new Vector3(-joint_left_shoulder.x, -joint_left_shoulder.y, joint_left_shoulder.z) * gap, Time.deltaTime*lerpSpeed);
            left_elbow.transform.position = Vector3.Lerp(left_elbow.transform.position, new Vector3(-joint_left_elbow.x, -joint_left_elbow.y, joint_left_elbow.z) * gap, Time.deltaTime*lerpSpeed);
            left_wrist.transform.position = Vector3.Lerp(left_wrist.transform.position, new Vector3(-joint_left_wrist.x, -joint_left_wrist.y, joint_left_wrist.z) * gap, Time.deltaTime*lerpSpeed);

            right_shoulder.transform.position = Vector3.Lerp(right_shoulder.transform.position, new Vector3(-joint_right_shoulder.x, -joint_right_shoulder.y, joint_right_shoulder.z) * gap, Time.deltaTime*lerpSpeed);
            right_elbow.transform.position = Vector3.Lerp(right_elbow.transform.position, new Vector3(-joint_right_elbow.x, -joint_right_elbow.y, joint_right_elbow.z) * gap, Time.deltaTime*lerpSpeed);
            right_wrist.transform.position = Vector3.Lerp(right_wrist.transform.position, new Vector3(-joint_right_wrist.x, -joint_right_wrist.y, joint_right_wrist.z) * gap, Time.deltaTime*lerpSpeed);
        
            left_hip.transform.position = Vector3.Lerp(left_hip.transform.position, new Vector3(-joint_left_hip.x, -joint_left_hip.y, joint_left_hip.z) * gap, Time.deltaTime*lerpSpeed);
            left_knee.transform.position = Vector3.Lerp(left_knee.transform.position, new Vector3(-joint_left_knee.x, -joint_left_knee.y, joint_left_knee.z) * gap, Time.deltaTime*lerpSpeed);
            left_ankle.transform.position = Vector3.Lerp(left_ankle.transform.position, new Vector3(-joint_left_ankle.x, -joint_left_ankle.y, joint_left_ankle.z) * gap, Time.deltaTime*lerpSpeed);

            right_hip.transform.position = Vector3.Lerp(right_hip.transform.position, new Vector3(-joint_right_hip.x, -joint_right_hip.y, joint_right_hip.z) * gap, Time.deltaTime*lerpSpeed);
            right_knee.transform.position = Vector3.Lerp(right_knee.transform.position, new Vector3(-joint_right_knee.x, -joint_right_knee.y, joint_right_knee.z) * gap, Time.deltaTime*lerpSpeed);
            right_ankle.transform.position = Vector3.Lerp(right_ankle.transform.position, new Vector3(-joint_right_ankle.x, -joint_right_ankle.y, joint_right_ankle.z) * gap, Time.deltaTime*lerpSpeed);
        }
    }

    
}
