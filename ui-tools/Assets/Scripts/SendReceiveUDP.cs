/*
 * This script sends a UDP packet to the remote host and receives a reply message from the remote host.
 * 
 * REFERENCE:   https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient?view=netframework-4.8
 * 
 * Modified 21 January 2020
 * by Anthony Fudd
 */

using UnityEngine;          //required to use Monobehaviours
using UnityEngine.UI;       //required to use onscreen GUI controls
using System;               //required to handle exceptions
using System.Text;          //required to encode the text strings for display
using System.Net;           //required to use IP endpoints and IP addresses
using System.Net.Sockets;   //required for the UdpClient class
using System.Threading;     //required to use threading

public class SendReceiveUDP : MonoBehaviour
{
    private UdpClient client;
    private IPEndPoint remoteEndPoint;
    private Thread receiveThread;
    private string remoteIP;        //remote IP address in string format
    private int remotePort;         //remote Port
    public InputField inputText;    //outgoing message (public so that the corresponding GUI input field can be assigned in the inspector).


    // Start is called before the first frame update
    void Start()
    {
        // define
        remoteIP = "192.168.1.18";
        remotePort = 5555;
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);     //Create IPEndpoint for the remote host.
        client = new UdpClient(remotePort);     //Create UDP client and bind to the desired port.
        inputText.text = "";        //Clear the GUI input field

        // status
        print("Sending to " + remoteIP + " on port: " + remotePort);

        receiveThread = new Thread(new ThreadStart(UdpReceiver));      //Create new thread with a Threadstart delegate that refers to the UDP receiving function
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // sendData
    private void sendString(string message)     //private function that keeps the 'message' field from being exposed in the On Click event in the inspector.
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

    public void sendText()      //public function assigned to the On Click button event in the inspector. Parameters are unnecessary since the argument comes from the input field.
    {
        sendString(inputText.text);
        print("Send Button Pressed!");
        print("Sent message " + inputText.text);
    }

    public void UdpReceiver()       //Receive a reply from the remote host to acknowledge receipt of the outgoing message
    {
        try
        {
            while (true)                                                //The 'while' statement allows the client to continually receive new datagrams. Otherwise, it will only work the first time.
            { 
                print("Waiting for UDP packet!");
                byte[] bytes = client.Receive(ref remoteEndPoint);      //This 'client.Receive' statement will automatically "block" execution of the rest of the method until a datagram is received.
                                                                        //IMPORTANT: This 'blocking' functionality can cause Unity to freeze without the use of threading.

                string text = Encoding.UTF8.GetString(bytes);           // Convert received data into UTF-8 string

                // status
                print("This is the message you received: " + text.ToString());
                print("Sent from " + remoteEndPoint.Address.ToString() + " on port number: " + remoteEndPoint.Port.ToString());
            }
            //loop back through the while statement to wait for the next datagram
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    // Unity Application Quit Function
    void OnApplicationQuit()
    {
        stopThread();
    }

    // Stop reading UDP messages
    private void stopThread()
    {
        if (receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
        client.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))     //Use the Update function to allow use of the return key to send data in addition to the SendButton in the GUI
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

