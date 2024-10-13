using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var server = new TcpServer(10001);
        server.Start();
        
        var client = new Client("http://localhost:10001");

        // Benutzer registrieren
        await client.RegisterUserAsync("Peter", "1234");

        // Benutzer einloggen
        await client.LoginUserAsync("Peter", "1234");
    }
}
