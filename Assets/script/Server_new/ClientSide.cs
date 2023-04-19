using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

using System.Runtime.Serialization.Formatters.Binary;


public class ClientSide : MonoBehaviour
{
    public static ClientSide instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    string clientName;

    [SerializeField]
    int PORT;
    [SerializeField]
    string IP;


    public bool socketReady = false;
    TcpClient socket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;

    char[] split_token = { ':' };


    public void ConnectToServer()
    {

        try
        {
            socket = new TcpClient(IP, PORT);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    private void Update()
    {
        if (socketReady && stream.DataAvailable)
        {
            if(!IsConnected(socket))
            {
                CloseSocket();
                return;
            }
            else
            {
                if(stream.DataAvailable)
                {
                    string all_data = reader.ReadLine();
                    string[] split_data = SplitData(all_data);

                    string type = split_data[0];
                    string data = split_data[1];
                    if(data != null)
                    {
                        OnIncomingData(type, data);
                    }
                }
            }
            
        }

        if(Input.GetKeyDown(KeyCode.Return) && !socketReady)
        {
            ConnectToServer();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && socketReady)
        {
            CloseSocket();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Send("Text", "Connection_TEST");
        }
        
    }

    bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }

    }

    void OnIncomingData(string type, string data)
    {
        if (type.Equals("Text"))
        {
            Debug.Log(data);
        }
        else
        {
            Debug.Log(data);
        }
    }

    string SendTypeData(string type)
    {
        return type + ":";
    }

    void Send(string type, string data)
    {
        if (!socketReady) return;

        data = SendTypeData(type) + data;
        writer.WriteLine(data);
        writer.Flush();
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    
    void CloseSocket()
    {
        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }

    string[] SplitData(string data)
    {
        return data.Split(split_token, 2);
    }
}
