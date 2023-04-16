using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class TTT
{
    [SerializeField]
    public string str;
    [SerializeField]
    public int tni = 0;
}


public class Server : MonoBehaviour
{
    public Text TTTServer;

    private static Server instance = null;

    TTT test;

    private void Awake()
    {

        test = new TTT();

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

    public InputField PortInput;

    List<ServerClient> clients;
    List<ServerClient> disconnectList;

    TcpListener server;
    bool serverStarted;

    public void ServerCreate()
    {
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        try
        {
            int port = PortInput.text == "" ? 7777 : int.Parse(PortInput.text);
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;
            Debug.Log($"서버가 {port}에서 시작되었습니다.");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        //Broadcast("%NAME", new List<ServerClient>() { clients[clients.Count - 1] });

        
    }

    private void Update()
    {
        if (!serverStarted) return;

        foreach (ServerClient c in clients)
        {
            if(!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectList.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if(s.DataAvailable)
                {
                    string data = new StreamReader(s, true).ReadLine();
                    if (data != null)
                        OnIncomingData(c, data);
                }
            }
        }

        for(int i = 0; i < disconnectList.Count-1; i++)
        {
            //Broadcast($"{disconnectList[i].clientName} 연결 끊김", clients);

            clients.Remove(disconnectList[i]);
            disconnectList.RemoveAt(i);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            test.str = "server_string";
            test.tni++;

            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream stream = new MemoryStream();

            formatter.Serialize(stream, test);
            byte[] byteArray = stream.ToArray();

            Broadcast2(byteArray,clients);
        }
    }

    void OnIncomingData(ServerClient c, string data)
    {
        if(data.Contains("&NAME"))
        {
            c.clientName = data.Split('|')[1];
            //Broadcast($"{c.clientName}이 연결되었습니다.",clients);
            return;
        }

        TTTServer.text = data;

        //Broadcast($"{c.clientName} : {data}", clients);

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

    void Broadcast(string data, List<ServerClient> cl)
    {
        foreach(var c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log($"쓰기 에러{e}");
            }
        }
    }

    void Broadcast2(byte[] data, List<ServerClient> cl)
    {
        foreach(var c in cl)
        {
            try
            {
                Stream stream = c.tcp.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
            catch (Exception e)
            {
                Debug.Log($"쓰기 에러{e}");
            }
        }
    }
}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "Client";
        tcp = clientSocket;
    }
}