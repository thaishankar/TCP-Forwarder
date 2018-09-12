using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Forwarder
{
    class Constants
    {
        public const string Localhost = "127.0.0.1";

        // Ports
        public const int HTTP_PORT = 80;
        public const int DEFAULT_SERVER_PORT = 50001;

        // Buffer sizes
        public const int CLIENT_READ_BUFFER = 1024;
        public const int CLIENT_WRITE_BUFFER = 1024;
        public const int SERVER_READ_BUFFER = 1024 * 1024; // 1 MB
        public const int SERVER_WRITE_BUFFER = 1024 * 1024; // 1 MB

    }
}
