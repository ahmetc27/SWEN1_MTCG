using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

public class TcpServer
{
    private TcpListener server;
    private Dictionary<string, string> users;

    public TcpServer(int port)
    {
        server = new TcpListener(IPAddress.Any, port);
        users = new Dictionary<string, string>
        {
            { "test", "test1" },
            { "test2", "test21" }
        };
    }

    public void Start()
    {
        server.Start();
        Console.WriteLine($"Server läuft auf Port {((IPEndPoint)server.LocalEndpoint).Port}...");

        while (true)
        {
            var client = server.AcceptTcpClient();
            using var stream = client.GetStream();
            using var writer = new StreamWriter(stream) { AutoFlush = true };
            using var reader = new StreamReader(stream);

            string? line = reader.ReadLine();
            if (string.IsNullOrEmpty(line)) continue;

            var httpParts = line.Split(' ');
            var method = httpParts[0];
            var path = httpParts[1];

            Console.WriteLine($"Methode: {method}, Pfad: {path}");

            var headers = ReadHeaders(reader, out int contentLength);
            string body = ReadBody(reader, contentLength);

            if (method == "POST" && path == "/sessions")
            {
                HandleLogin(body, writer);
            }
            else if (method == "POST" && path == "/users")
            {
                HandleRegister(body, writer);
            }
            else
            {
                SendNotFound(writer);
            }
        }
    }

    private Dictionary<string, string> ReadHeaders(StreamReader reader, out int contentLength)
    {
        var headers = new Dictionary<string, string>();
        contentLength = 0;
        string? line;
        while ((line = reader.ReadLine()) != null && line != "")
        {
            var parts = line.Split(':', 2);
            if (parts.Length == 2)
            {
                headers[parts[0].Trim()] = parts[1].Trim();
                if (parts[0].Trim().Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
                {
                    contentLength = int.Parse(parts[1].Trim());
                }
            }
        }
        return headers;
    }

    private string ReadBody(StreamReader reader, int contentLength)
    {
        if (contentLength > 0)
        {
            char[] buffer = new char[contentLength];
            reader.Read(buffer, 0, contentLength);
            return new string(buffer);
        }
        return string.Empty;
    }

    private void HandleLogin(string body, StreamWriter writer)
    {
        try
        {
            var loginInfo = JsonSerializer.Deserialize<Dictionary<string, string>>(body);
            if (loginInfo != null && users.TryGetValue(loginInfo["Username"], out var password) && password == loginInfo["Password"])
            {
                var token = $"{loginInfo["Username"]}-mtcgToken";
                var response = JsonSerializer.Serialize(new { Token = token });

                writer.WriteLine("HTTP/1.1 200 OK");
                writer.WriteLine("Content-Type: application/json");
                writer.WriteLine($"Content-Length: {response.Length}");
                writer.WriteLine();
                writer.WriteLine(response);
            }
            else
            {
                writer.WriteLine("HTTP/1.1 401 Unauthorized");
                writer.WriteLine("Content-Type: text/plain");
                writer.WriteLine("Content-Length: 22");
                writer.WriteLine();
                writer.WriteLine("Invalid login credentials");
            }
        }
        catch (JsonException)
        {
            writer.WriteLine("HTTP/1.1 400 Bad Request");
            writer.WriteLine("Content-Type: text/plain");
            writer.WriteLine("Content-Length: 22");
            writer.WriteLine();
            writer.WriteLine("Invalid request format");
        }
    }

    private void HandleRegister(string body, StreamWriter writer)
    {
        try
        {
            var registerInfo = JsonSerializer.Deserialize<Dictionary<string, string>>(body);
            if (registerInfo != null && registerInfo.ContainsKey("Username") && registerInfo.ContainsKey("Password"))
            {
                string username = registerInfo["Username"];
                string password = registerInfo["Password"];

                if (users.ContainsKey(username))
                {
                    writer.WriteLine("HTTP/1.1 409 Conflict");
                    writer.WriteLine("Content-Type: text/plain");
                    writer.WriteLine("Content-Length: 23");
                    writer.WriteLine();
                    writer.WriteLine("Username already exists");
                }
                else
                {
                    users.Add(username, password);
                    writer.WriteLine("HTTP/1.1 201 Created");
                    writer.WriteLine("Content-Type: text/plain");
                    writer.WriteLine("Content-Length: 21");
                    writer.WriteLine();
                    writer.WriteLine("User registered successfully");
                }
            }
            else
            {
                writer.WriteLine("HTTP/1.1 400 Bad Request");
                writer.WriteLine("Content-Type: text/plain");
                writer.WriteLine("Content-Length: 33");
                writer.WriteLine();
                writer.WriteLine("Username and password are required");
            }
        }
        catch (JsonException)
        {
            writer.WriteLine("HTTP/1.1 400 Bad Request");
            writer.WriteLine("Content-Type: text/plain");
            writer.WriteLine("Content-Length: 22");
            writer.WriteLine();
            writer.WriteLine("Invalid request format");
        }
    }

    private void SendNotFound(StreamWriter writer)
    {
        writer.WriteLine("HTTP/1.1 404 Not Found");
        writer.WriteLine("Content-Type: text/plain");
        writer.WriteLine("Content-Length: 13");
        writer.WriteLine();
        writer.WriteLine("404 Not Found");
    }
}
