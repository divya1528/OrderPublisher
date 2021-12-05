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

                // retry settings:
                // Receive acknowledgement from all sync replicas
                Acks = Acks.All,
                // Number of times to retry before giving up
                MessageSendMaxRetries = 3,
                // Duration to retry before next attempt
                RetryBackoffMs = 1000,
                // Set to true if you don't want to reorder messages on retry
                EnableIdempotence = true
            };
        }
        public async Task WriteMessageAsync(string message)
        {
            using var producer = new ProducerBuilder<long, string>(_producerConfig)
                 .SetKeySerializer(Serializers.Int64)
                 .SetValueSerializer(Serializers.Utf8)
                 .SetLogHandler((_, message) =>
                     Console.WriteLine($"Facility: {message.Facility}-{message.Level} Message: {message.Message}"))
                 .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}. Is Fatal: {e.IsFatal}"))
                 .Build();
            try
            {

                    var deliveryReport = await producer.ProduceAsync("orders",
                        new Message<long, string>
                        {
                            Key = DateTime.UtcNow.Ticks,
                            Value = message
                        });

                    Console.WriteLine($"Message sent (value: '{message}'). Delivery status: {deliveryReport.Status}");
                    if (deliveryReport.Status != PersistenceStatus.Persisted)
                    {
                        // delivery might have failed after retries. This message requires manual processing.
                        Console.WriteLine(
                            $"ERROR: Message not ack'd by all brokers (value: '{message}'). Delivery status: {deliveryReport.Status}");
                    }

            }
            catch (ProduceException<long, string> e)
            {
                // Log this message for manual processing.
                Console.WriteLine($"Permanent error: {e.Message} for message (value: '{e.DeliveryResult.Value}')");
                Console.WriteLine("Exiting producer...");
            }
        }
    }
}
