using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Configuration;
using N5Company.Domain.Models;

namespace N5Company.Api.Helpers
{
	public class KafkaProducerService
	{
		private readonly IProducer<Null, string> _producer;

		public KafkaProducerService(IConfiguration configuration)
		{
			var bootstrapServers = configuration["Kafka:BootstrapServers"];

			var config = new ProducerConfig { BootstrapServers = bootstrapServers };

			_producer = new ProducerBuilder<Null, string>(config).Build();
		}

		public async Task ProduceAsync(string topic, string message)
		{
			var dr = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

			Console.WriteLine($"de '{dr.Value}' a '{dr.TopicPartitionOffset}'");
		}
	}
}
