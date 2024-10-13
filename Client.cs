using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Client
{
    private readonly HttpClient httpClient;
    private readonly string baseUrl;

    public Client(string baseUrl)
    {
        httpClient = new HttpClient();
        this.baseUrl = baseUrl;
    }

    public async Task RegisterUserAsync(string username, string password)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { Username = username, Password = password }), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync($"{baseUrl}/users", content);
        
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("User registered successfully.");
        }
        else
        {
            Console.WriteLine($"Registration failed: {response.StatusCode}");
        }
    }

    public async Task LoginUserAsync(string username, string password)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { Username = username, Password = password }), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync($"{baseUrl}/sessions", content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Login successfully. Response: {responseBody}");
        }
        else
        {
            Console.WriteLine($"Login failed: {response.StatusCode}");
        }
    }
}
