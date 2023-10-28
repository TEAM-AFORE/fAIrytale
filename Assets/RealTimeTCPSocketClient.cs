using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class RealTimeTCPSocketClient : MonoBehaviour
{

    private TcpClient client;
    private NetworkStream stream;
    private string serverIP = "127.0.0.1";
    private int serverPort = 50000;



    private void Start()
    {
        ConnectSocket();
    }

    void Update()
    {

    }


    private void ConnectSocket()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();

            Debug.Log("Connected to server: " + serverIP + ":" + serverPort);

            // 클라이언트 데이터 수신을 처리하는 코루틴 시작
            StartCoroutine(ReceiveData());
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to connect to server: " + e.Message);
        }
    }

    private IEnumerator ReceiveData()
    {
        byte[] buffer = new byte[1024];
        string copy = "0";

        while (true)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log("Received from server: " + receivedData);

                    if (copy != receivedData)
                    {
                        copy = receivedData;

                        // 클라이언트.txt 파일로 데이터 저장
                        string filePath = Application.dataPath + "/client.txt";
                        System.IO.File.WriteAllText(filePath, receivedData, Encoding.UTF8);
                        Debug.Log("Saved to client.txt in : " + filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                // 다른 예외 처리 코드
                Console.WriteLine("예외가 발생했습니다: " + ex.Message);
            }


        }


        stream.Close();
        client.Close();
    }

    public void SendData(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent to server: " + message);
        }
        catch (Exception e)
        {
            Debug.LogError("Error while sending data: " + e.Message);
        }
    }

    // Unity 애플리케이션이 종료될 때 연결 해제
    private void OnApplicationQuit()
    {
        if (client != null && client.Connected)
        {
            stream.Close();
            client.Close();
        }
    }
}

