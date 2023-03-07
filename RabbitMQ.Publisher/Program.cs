using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://oaoefwoc:GakX6_Y-JQyumxc5fU9z998UJNZ5lHLO@rattlesnake.rmq.cloudamqp.com/oaoefwoc");//RabbitMQ Manager içinde CloudAMQP URL i

            using var connection = factory.CreateConnection();//bağlantı açılır.

            var channel = connection.CreateModel();//kanal oluşturulur.

            channel.QueueDeclare("hello-queue", true, false, false);//Kuyruk oluşturulur.
                                                                    //1. rabbitMQ ismi 2. Kuyruk fiziksel  olarak kaydedilsinmi? 3. true olur ise sadece bu kanaldan ulaşılabilsin. false ise farklı yerlerden erişilebilsin.
                                                                    //4. kuyruk down olunca silinsin mi? 5. 


            string message = "Hello world";
            //RabbitMQ ya mesajlar byte dizisi olarak gönderilir.
            var messageBody = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

            Console.WriteLine("Mesaj gönderilmiştir.");
            Console.ReadLine();
        }
    }
}
