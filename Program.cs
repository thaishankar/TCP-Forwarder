using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace TCP_Forwarder
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            new Thread(Server).Start();
            Thread.Sleep(100);
            new Thread(Client).Start();

        }

        static void Server()
        {
            Listener newListener = new Listener(Constants.DEFAULT_SERVER_PORT);
            newListener.Start();
            newListener.Accept();
            newListener.Stop();
        }

        static void Client()
        {
            for ( int i = 0; i < 5; i++)
            {
                Client newClient = new Client();
                newClient.Send(String.Format("Hello from Client {0}", i));
                Console.WriteLine("Client Received {0}", newClient.Receive());
                newClient.Close();
            }

        }
    }
}
