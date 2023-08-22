using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://quest:quest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sender App";
    
    
IConnection cnn = factory.CreateConnection();

IModel channel = cnn.CreateModel();

string exhangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exhangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exhangeName, routingKey, null);

byte[] messagebodyBytes = Encoding.UTF8.GetBytes("Hello Yutube");
channel.BasicPublish(exhangeName, routingKey, null, messagebodyBytes);

channel.Close();
cnn.Close();