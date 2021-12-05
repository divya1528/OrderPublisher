using ApplicationCore.Abstract;
using Confluent.Kafka;
using OrderPublisher.Abstract;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OrderPublisher.Concrete
{
    public class ProducerWrapper : IProducerWrapper
    {
        readonly ProducerConfig _producerConfig;
        IConfigSettings _config;

        public ProducerWrapper(IConfigSettings config)
        {
            _config = config;

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = _config.BootstrapServer,
                EnableDeliveryReports = true,
                ClientId = Dns.GetHostName(),
                Debug = "msg",
                Acks = Acks.All,
                MessageSendMaxRetries = 3,
                RetryBackoffMs = 1000,
                // Set to true if you don't want to reorder messages on retry
                EnableIdempotence = true
            };
        }
        public async Task WriteMessageAsync(string message)
        {
            using var producer = new ProducerBuilder<string, string>(_producerConfig)
                 .Build();
            try
            {
                var messg = new Message<string, string> { Key = null, Value = message };
                var deliveryReport = await producer.ProduceAsync("orders", messg);

                if (deliveryReport.Status != PersistenceStatus.Persisted)
                {
                    // delivery might have failed after retries. This message requires manual processing.
                    Console.WriteLine(
                        $"ERROR: Message not ack'd by all brokers (value: '{message}'). Delivery status: {deliveryReport.Status}");
                }

            }
            catch (ProduceException<long, string> e)
            {
                Console.WriteLine($"Permanent error: {e.Message} for message (value: '{e.DeliveryResult.Value}')");
            }
        }
    }
}
