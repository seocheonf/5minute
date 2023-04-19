using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

using System.Runtime.Serialization.Formatters.Binary;


public class Client : MonoBehaviour
{
    public Text TTTClient;

    public static Client instance = null;

    [SerializeField]
    public GameObject ggg;

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

    public InputField IPInput, PortInput, NickInput;
    string clientName;

    public bool socketReady;
    TcpClient socket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;

    public void ConnectToServer()
    {
        if (socketReady) return;

        string ip = IPInput.text == "" ? "127.0.0.1" : IPInput.text;
        int port = PortInput.text == "" ? 7777 : int.Parse(PortInput.text);

        try
        {
            socket = new TcpClient(ip, port);
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
        /*
        if(socketReady && stream.DataAvailable)
        {
            string data = reader.ReadLine();
            if(data!=null)
            {
                OnIncomingData(data);
            }
        }
        */
        

        if (socketReady && stream.DataAvailable)
        {
            byte[] buffer = new byte[socket.ReceiveBufferSize];
            stream.Read(buffer, 0, buffer.Length);

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memorystream = new MemoryStream(buffer);

            var test = formatter.Deserialize(memorystream);
            Debug.Log(test.GetType());
            OnIncomingData2(test.GetType(), test);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Send("client");
        }
    }

    void OnIncomingData2(Type type, object data)
    {
        if(type.ToString().Equals("TTT"))
        {
            TTT test = (TTT)data;
            Debug.Log(test.str + test.tni);
        }
        else if(type.ToString().Equals("GameObject"))
        {
            ggg = (GameObject)data;
        }
    }



    void OnIncomingData(string data)
    {
        TTTClient.text = data;
        if(data == "%NAME")
        {
            clientName = NickInput.text == "" ? "Guest" + UnityEngine.Random.Range(1000, 10000) : NickInput.text;
            Send($"&NAME|{clientName}");
            return;
        }
    }

    void Send(string data)
    {
        if (!socketReady) return;
        writer.WriteLine(data);
        writer.Flush();
    }

    public void OnSendButton(InputField SendInput)
    {
        if (!Input.GetButtonDown("Submit")) return;
        SendInput.ActivateInputField();
        if (SendInput.text.Trim() == "") return;

        string message = SendInput.text;
        SendInput.text = "";
        Send(message);
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    void CloseSocket()
    {
        if (!socketReady) return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }

}
