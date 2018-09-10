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
                    Thread clientThread = new Thread(() => ReadWrite(client));
                    clientThread.Start();
                }
            }
        }

        public static void ReadWrite(TcpClient client)
        {
            Console.WriteLine("Connected!");

            Byte[] bytes = new Byte[256];
            NetworkStream stream = client.GetStream();
            String data = null;

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
            client.Close();
        }


    }
}
