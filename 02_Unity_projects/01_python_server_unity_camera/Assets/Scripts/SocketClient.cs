using UnityEngine;
using System.Text;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

[System.Serializable] public class Joint
{
    public int index;
    public float x;
    public float y;
    public float z;
    public float visibility;
}

[System.Serializable]  public class JointList
{
    public Joint[] joint;
}

public class SocketClient : MonoBehaviour
{
    public GameObject obj;
    public WebcamHandler webcamHandler;
    public string serverIp = "localhost";
    public int serverPort = 5000;
    private TcpClient clientSocket;
    private NetworkStream stream;
    bool isSending = false;
    private Coroutine coSendImageE;
    public JointList jointList = new JointList();
        
    // 서버연결
    // Connect 버튼 onClick과 연결
    public void ConnectToServer()
    {
        try
        {
            clientSocket = new TcpClient(serverIp, serverPort);
            stream = clientSocket.GetStream();
            Debug.Log("server connected");
        }
        
        catch (SocketException e)
        {
            Debug.LogError(e);
        }
    }

    // 서버연결 종료
    // Disconnect 버튼 onClick과 연결
    public void DisconnectToServer()
    {
        try
        {
            StopCoroutine(coSendImageE);
            stream.Close();
            clientSocket.Close();
            isSending = false;
            Debug.Log("server disconnected");
        }
        
        catch (SocketException e)
        {
            Debug.LogError(e);
        }
    }

    // 서버에 이미지(jpg) 전송
    // Send 버튼 onClick과 연결
    private void SendImage() 
    {
        if(!isSending)
        {
            isSending = true;
            byte[] data = webcamHandler.MakeImgtoByte();
            stream = clientSocket.GetStream();
            if (clientSocket == null) { return; }
            try { 			
                if (stream.CanWrite)
                {                           
                    stream.Write(data, 0, data.Length);                 
                    isSending = false;
                    // 서버에 보낸 HPE 결과 읽어옴.
                    RecvJson();
                }         
            } 		
            catch (SocketException socketException) {             
                Debug.Log("Socket exception: " + socketException);
                isSending = false;
            }     
        }
	}


    private IEnumerator CoSendImage()
    {
        while(true)
        {
            SendImage();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ButtonSendImage()
    {
        coSendImageE = StartCoroutine(CoSendImage());
    }

    private void RecvJson()
    {
        if(stream.DataAvailable)
        {
            if(clientSocket == null) { return; }
            try
            {
                byte[] buffer = new byte[6144];
                int numOfBytesRead = 0;
                StringBuilder landmarkJson = new StringBuilder();
                do
                {
                    numOfBytesRead = stream.Read(buffer, 0, buffer.Length);
                    landmarkJson.AppendFormat("{0}", Encoding.ASCII.GetString(buffer, 0, numOfBytesRead));
                }
                while(stream.DataAvailable);

                jointList = JsonUtility.FromJson<JointList>(landmarkJson.ToString());
            }
            catch (SocketException socketException) {             
                Debug.Log("Socket exception: " + socketException);
            } 
        }
    }
}