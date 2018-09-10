using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCP_Forwarder
{
    class Client
    {
        private int serverPort;
        TcpClient _client;
        NetworkStream stream;

        public Client(int serverPort)
        {
            this.serverPort = serverPort;
            _client = new TcpClient("127.0.0.1", this.serverPort);
            stream = _client.GetStream();
        }

        public void Send(String message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
     
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);
        }

        public string Receive()
        {
            Byte[] data = new Byte[256];

            String responseData = String.Empty;

            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);
            return responseData;
        }

        public void Close()
        {
            stream.Close();
            _client.Close();
        }
    }
}
