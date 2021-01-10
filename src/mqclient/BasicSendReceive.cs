using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace mqclient {
    public class BasicSendReceive : IDisposable {
        IConnection Connection;
        IModel Channel;
        public BasicSendReceive(string hostName) {
            var factory = new ConnectionFactory() { HostName = hostName };

            this.Connection = factory.CreateConnection();
            this.Channel = Connection.CreateModel();
        }

        public void Send(string queueName, byte[] body) {
            this.Channel.QueueDeclare(queue: queueName, 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);
            
            this.Channel.BasicPublish(exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body);
        }

        public void Receive(string queueName) {
            var consumer = new EventingBasicConsumer(this.Channel);
            consumer.Received += (model, ea) => {
                var body = ea.Body.ToArray();
            };

            this.Channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public void Dispose() {
            this.Channel.Dispose();
            this.Connection.Dispose();
        }
    }
}