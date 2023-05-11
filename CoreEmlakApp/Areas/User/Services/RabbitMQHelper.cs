using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CoreEmlakApp.Areas.User.Services
{
    public class RabbitMQHelper
    {
        private readonly ConnectionFactory _factory;
        private readonly IModel _channel;

        public RabbitMQHelper()
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new Uri("amqps://lzezpyyw:dySWOg9CFYF8iVh39FcNSZzZ0-SZJNHH@toad.rmq.cloudamqp.com/lzezpyyw");
            var connection = _factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "password_reset_request", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendPasswordResetRequest(string email, string passwordResetLink)
        {
            string message = $"{email} | {passwordResetLink}";
            var body = Encoding.UTF8.GetBytes(message) ;
            _channel.BasicPublish(exchange: "",routingKey: "password_reset_request", basicProperties: null ,body:body);
        }

        public void ConsumePasswordResetRequest(Action<string, string> onReceived) {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                string[] parts = message.Split('|');
                string email = parts[0];
                string passwordResetLink = parts[1];

                onReceived(email, passwordResetLink);
            };

            _channel.BasicConsume(queue: "password_reset_request", autoAck: true, consumer: consumer);
        }
       
    }
}
