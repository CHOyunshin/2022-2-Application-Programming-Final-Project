using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public SocketClient socketClient;
    public JointList jointList;
    public Transform nose;
    public Transform left_shoulder;
    public Transform left_elbow;
    public Transform left_wrist;
    public Transform right_shoulder;
    public Transform right_elbow;
    public Transform right_wrist;
    public Transform left_hip;
    public Transform left_knee;
    public Transform left_ankle;
    public Transform right_hip;
    public Transform right_knee;
    public Transform right_ankle;
    

    public float gap = 1.3f;
    public float lerpSpeed = 45f;

    void Update()
    {
        jointList = socketClient.jointList;
        if(jointList.joint.Length > 0)
        {
            TransformSphere(jointList);
            DrawLineAll();
        }
    }

    // Draw line between joint
    void DrawLine(Transform start, Transform end)
    {
        LineRenderer lr = start.GetComponent<LineRenderer>();
        lr.startWidth = 0f;
        lr.endWidth = 0.04f;

        lr.SetPosition(0, start.position);
        lr.SetPosition(1, end.position);
    }

    // Draw all lines between joints
    void DrawLineAll()
    {
        DrawLine(left_wrist, left_elbow);
        DrawLine(left_elbow, left_shoulder);
        DrawLine(left_shoulder, right_shoulder);
        DrawLine(right_wrist, right_elbow);
        DrawLine(right_elbow, right_shoulder);
        DrawLine(right_shoulder, right_hip);
        DrawLine(right_hip, left_hip);
        DrawLine(left_hip, left_shoulder);
        DrawLine(left_ankle, left_knee);
        DrawLine(left_knee, left_hip);
        DrawLine(right_ankle, right_knee);
        DrawLine(right_knee, right_hip);
    }
    
    // Transform joint position
    void TransformPoint(Transform joint, Vector3 pos)
    {
        Joint j_left_hip = jointList.joint[23];
        Joint j_right_hip = jointList.joint[24];

        Vector3 left_hip_vec = new Vector3(j_left_hip.x, j_left_hip.y, j_left_hip.z);
        Vector3 right_hip_vec = new Vector3(j_right_hip.x, j_right_hip.y, j_right_hip.z);
        Vector3 offset = (left_hip_vec + right_hip_vec) / 2f;
        
        pos = pos - offset;
        
        joint.position = Vector3.Lerp(joint.position, pos * -1 * gap, Time.deltaTime*lerpSpeed);
    }

    // Transform all joints position
    void TransformSphere(JointList jointList)
    {
        Joint j_nose = jointList.joint[0];

        Joint j_left_shoulder = jointList.joint[11];
        Joint j_left_elbow = jointList.joint[13];
        Joint j_left_wrist = jointList.joint[15];

        Joint j_right_shoulder = jointList.joint[12];
        Joint j_right_elbow = jointList.joint[14];
        Joint j_right_wrist = jointList.joint[16];

        Joint j_left_hip = jointList.joint[23];
        Joint j_left_knee = jointList.joint[25];
        Joint j_left_ankle = jointList.joint[27];

        Joint j_right_hip = jointList.joint[24];
        Joint j_right_knee = jointList.joint[26];
        Joint j_right_ankle = jointList.joint[28];
        
        // // z값 포함
        TransformPoint(nose, new Vector3(j_nose.x, j_nose.y, j_nose.z));

        TransformPoint(left_shoulder, new Vector3(j_left_shoulder.x, j_left_shoulder.y, j_left_shoulder.z));
        TransformPoint(left_elbow, new Vector3(j_left_elbow.x, j_left_elbow.y, j_left_elbow.z));
        TransformPoint(left_wrist, new Vector3(j_left_wrist.x, j_left_wrist.y, j_left_wrist.z));

        TransformPoint(right_shoulder, new Vector3(j_right_shoulder.x, j_right_shoulder.y, j_right_shoulder.z));
        TransformPoint(right_elbow, new Vector3(j_right_elbow.x, j_right_elbow.y, j_right_elbow.z));
        TransformPoint(right_wrist, new Vector3(j_right_wrist.x, j_right_wrist.y, j_right_wrist.z));

        TransformPoint(left_hip, new Vector3(j_left_hip.x, j_left_hip.y, j_left_hip.z));
        TransformPoint(left_knee, new Vector3(j_left_knee.x, j_left_knee.y, j_left_knee.z));
        TransformPoint(left_ankle, new Vector3(j_left_ankle.x, j_left_ankle.y, j_left_ankle.z));

        TransformPoint(right_hip, new Vector3(j_right_hip.x, j_right_hip.y, j_right_hip.z));
        TransformPoint(right_knee, new Vector3(j_right_knee.x, j_right_knee.y, j_right_knee.z));
        TransformPoint(right_ankle, new Vector3(j_right_ankle.x, j_right_ankle.y, j_right_ankle.z));

        // // z값을 0으로 설정
        // TransformPoint(nose, new Vector3(j_nose.x, j_nose.y, 0));

        // TransformPoint(left_shoulder, new Vector3(j_left_shoulder.x, j_left_shoulder.y, 0));
        // TransformPoint(left_elbow, new Vector3(j_left_elbow.x, j_left_elbow.y, 0));
        // TransformPoint(left_wrist, new Vector3(j_left_wrist.x, j_left_wrist.y, 0));

        // TransformPoint(right_shoulder, new Vector3(j_right_shoulder.x, j_right_shoulder.y, 0));
        // TransformPoint(right_elbow, new Vector3(j_right_elbow.x, j_right_elbow.y, 0));
        // TransformPoint(right_wrist, new Vector3(j_right_wrist.x, j_right_wrist.y, 0));

        // TransformPoint(left_hip, new Vector3(j_left_hip.x, j_left_hip.y, 0));
        // TransformPoint(left_knee, new Vector3(j_left_knee.x, j_left_knee.y, 0));
        // TransformPoint(left_ankle, new Vector3(j_left_ankle.x, j_left_ankle.y, 0));

        // TransformPoint(right_hip, new Vector3(j_right_hip.x, j_right_hip.y, 0));
        // TransformPoint(right_knee, new Vector3(j_right_knee.x, j_right_knee.y, 0));
        // TransformPoint(right_ankle, new Vector3(j_right_ankle.x, j_right_ankle.y, 0));
    }
}
