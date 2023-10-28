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

            // Ŭ���̾�Ʈ ������ ������ ó���ϴ� �ڷ�ƾ ����
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

                        // Ŭ���̾�Ʈ.txt ���Ϸ� ������ ����
                        string filePath = Application.dataPath + "/client.txt";
                        System.IO.File.WriteAllText(filePath, receivedData, Encoding.UTF8);
                        Debug.Log("Saved to client.txt in : " + filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                // �ٸ� ���� ó�� �ڵ�
                Console.WriteLine("���ܰ� �߻��߽��ϴ�: " + ex.Message);
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

    // Unity ���ø����̼��� ����� �� ���� ����
    private void OnApplicationQuit()
    {
        if (client != null && client.Connected)
        {
            stream.Close();
            client.Close();
        }
    }
}

