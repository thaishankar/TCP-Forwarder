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
        private string serverIp;
        TcpClient _client;
        NetworkStream stream;

        public Client(string serverIp = Constants.Localhost, int serverPort = Constants.DEFAULT_SERVER_PORT)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            _client = new TcpClient(this.serverIp, this.serverPort);
            stream = _client.GetStream();
        }

        public void Send(String message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
     
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Client Sent: {0}", message);
        }

        public string Receive()
        {
            Byte[] data = new Byte[Constants.CLIENT_READ_BUFFER];

            String responseData = String.Empty;

            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Client Received: {0}", responseData);
            return responseData;
        }

        public void Close()
        {
            stream.Close();
            _client.Close();
        }
    }
}
