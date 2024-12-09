using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

Console.WriteLine("[*] Aguardando mensagens.");

var consumidor = new EventingBasicConsumer(channel);
consumidor.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var mensagem = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[x] Recebido: {mensagem}");
};

channel.BasicConsume(queue: "nova_fila",
                     autoAck: true,
                     consumer: consumidor);

Console.WriteLine("Aperte [Enter] para sair.");
Console.ReadLine();
