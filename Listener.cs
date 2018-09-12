using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace TCP_Forwarder
{
    class Listener
    {
        TcpListener _server = null;
        private int port;
        private IPAddress ip;

        public Listener(int port)
        {
            this.port = port;
            this.ip = IPAddress.Parse("127.0.0.1");
            this._server = new TcpListener(this.ip, this.port);
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }

        public void Accept()
        {
            while(true)
            {
                TcpClient client = _server.AcceptTcpClient();

                if (client.Connected)
                {
                    Thread clientThread = new Thread(() => RequestRespond(client));
                    clientThread.Start();
                }
            }
        }

        public static void RequestRespond(TcpClient client)
        {
            Console.WriteLine("Connected!");

            byte[] bytes = new byte[Constants.SERVER_READ_BUFFER];
            NetworkStream stream = client.GetStream();
            
            
            while(true)
            {
                int bytesRead = 0;               
                do
                {
                    bytesRead = Read(stream, bytes);
                    if(bytesRead <= 0)
                    {
                        goto Finished;
                    }

                    byte[] msg =  ProcessMsg(bytes, bytesRead);

                    int bytesWritten = Write(stream, msg);

                } while (bytesRead == Constants.SERVER_READ_BUFFER);
            }
        Finished:
            stream.Close();
            client.Close();
        }

        public static int Read(NetworkStream stream, Byte[] bytes)
        {
            int i = 0;

            // Loop to receive all the data sent by the client.
            try
            {
                i = stream.Read(bytes, 0, bytes.Length);
            }
            catch(SocketException)
            {
                return -1;
            }
            return i;
        }

        public static byte[] ProcessMsg(byte[] bytes, int bytesRead)
        {
            string data = string.Empty;

            data = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
            Console.WriteLine("Server Received len {0}: {1}", bytes.Length, data);
            data.ToUpper();

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
            return msg;
        }

        public static int Write(NetworkStream stream, byte[] msg)
        {


            // Send back a response.
            stream.Write(msg, 0, msg.Length);
            Console.WriteLine("Server Sent: {0}", msg);
            return msg.Length;

        }


    }
}
