/*
 * This script sends a UDP packet to a remote host as a one way transmission.
 * 
 * REFERENCE:   https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient?view=netframework-4.8
 * 
 * Modified 21 January 2020
 * by Anthony Fudd
 */

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class SendUDP : MonoBehaviour
{
    public UdpClient client;
    private IPEndPoint remoteEndPoint;
    private string remoteIP;        //remote IP address in string format
    private int remotePort;          //remote Port
    public InputField inputText;    //outgoing message


    // Start is called before the first frame update
    void Start()
    {
        // define
        client = new UdpClient(5555);
        remoteIP = "192.168.1.18";
        remotePort = 5555;
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        inputText.text = "";

        // status
        print("Sending to " + remoteIP + " on port: " + remotePort);
    }

    // sendData
    private void sendString(string message)
    {
        try
        {
            if (message != "")
            {
                //Convert message to UTF-8 format
                byte[] data = Encoding.UTF8.GetBytes(message);

                //Send message
                client.Send(data, data.Length, remoteEndPoint);
            }
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    public void sendText()
    {
        sendString(inputText.text);
        print("Send Button Pressed!");
        print("Sent message " + inputText.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            if (inputText.text != "")
            {
                sendString(inputText.text);
                print("Return Key Pressed!");
                print("Sent message " + inputText.text);
            }
        }
    }
}
