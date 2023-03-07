using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.Subscriber
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
            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("hello-queue", true, consumer);
            //1. bu kanal hangi kuyruğu izliycek
            //2. true olursa kuyruktan 1 mesaj gönderildiğinde doğruda işlense yanlışta işlense siler, false ise kuyruktan silme ben sana silceğin zamanı söyliycem anlamındadır.
            //normalde 2. false olur çünkü yanlış gönderilebilir.
            consumer.Received += (object sender, BasicDeliverEventArgs e)=> 
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen Mesaj:" + message);
                Console.ReadLine();
            };
        }
    }
}
