using System.Text;
using Newtonsoft.Json;
using Planning_Poker.Dto;
using RabbitMQ.Client;

namespace Planning_Poker.RabbitMQ
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel, VotesDto votesDto)
        {
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

                var message = new { Name = "AllVotes", Message = $"Hello! Vote Publish: {votesDto.Id}" };
            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "demo-queue", null, body);
        }
    }
}
