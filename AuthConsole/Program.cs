// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using AuthApp.Dtos;

using HttpClient client = new();

const string authApUrl = "http://localhost:5292";

while (true)
{
    Console.WriteLine("Jaką operację chcesz wykonać?");
    Console.WriteLine("1. Utwórz konto");
    Console.WriteLine("2. Zaloguj");
    Console.WriteLine("3. Wyświetl urzytkowników");
    Console.WriteLine("4. Zamknij aplikację");
    Console.WriteLine("Wprowadź numer operacji: ");
    var operationNumberReading = Console.ReadKey().KeyChar;
    int operationNumber;
    var parsingSuccess = int.TryParse(operationNumberReading.ToString(), out operationNumber);
    if (parsingSuccess && operationNumber is > 0 and < 5)
    {
        switch (operationNumber)
        {
            case 1:
                await Register();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                Console.Clear();
                Console.WriteLine("Do widzenia!");
                Thread.Sleep(3000);
                return;
        }
    }
    else
    {
        Console.WriteLine("Podano błędny numer!");
    }
    Console.Clear();
}


async Task Register()
{
    Console.Write("Podaj Login: ");
    var login = Console.ReadLine()!;
    Console.Write("Podaj Hasło: ");
    var password = Console.ReadLine()!;
    Console.Write("Podaj email: ");
    var email = Console.ReadLine()!;

    var requestUrl = $"{authApUrl}/register";
    var registerDto = new RegisterDto{Login = login, Password = password, Email = email};
    try
    {
        var jsonRegisterDto = JsonSerializer.Serialize(registerDto);
        var stringContent = new StringContent(jsonRegisterDto);
        var result = await client.PostAsync(requestUrl, stringContent);
        if (!result.IsSuccessStatusCode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Kod błędu: {result.StatusCode.ToString()}");
            Console.WriteLine(result.Content.ReadFromJsonAsync<string>());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    Console.WriteLine("Naciśnij jakikowliek klawisz, aby przejść dalej");
    Console.ReadKey();
}