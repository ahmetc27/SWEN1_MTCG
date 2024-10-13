using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        var server = new TcpServer(10001);
        server.Start();
    }
}
