using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
   .WithUrl("https://localhost:5000/chat")
   .Build();

using var registration = connection.On<string>("MessageToClient", OnMessageFromServer);

Console.WriteLine("Конфигурация выполнена. Ожидаем запуск сервера.\r\nНажмите Enter когда сервер будет запущен.");
Console.ReadLine();

await connection.StartAsync();

Console.WriteLine("Подключение к серверу выполнено.");

while (true)
{
	var message = Console.ReadLine();
	await connection.InvokeAsync("SendMessage", message);
}

static void OnMessageFromServer(string Message)
{
	Console.WriteLine("Message from server: {0}", Message);
}