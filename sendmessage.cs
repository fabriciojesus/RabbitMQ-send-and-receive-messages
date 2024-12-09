using RabbitMQ.Client;
using System;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "nova_fila",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine("Digite sua mensagem e aperte <ENTER>");

while (true)
{
    string mensagem = Console.ReadLine();

    if (mensagem == "")
        break;

    var body = Encoding.UTF8.GetBytes(mensagem);

    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "nova_fila",
                         basicProperties: null,
                         body: body);

    Console.WriteLine($"[x] Enviado: {mensagem}");
}
