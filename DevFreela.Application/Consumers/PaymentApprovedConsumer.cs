﻿using DevFreela.Application.IntegrationEvents;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Consumers
{
    public class PaymentApprovedConsumer : BackgroundService
    {
        private const string PAYMENT_APPROVED_QUEUE = "PaymentsApproved";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider; // Necessario para acessar servicos injetados na aplicacao, assim rodará infinitamente

        public PaymentApprovedConsumer(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;

            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Garantir que a fila esteja criada
            _channel.QueueDeclare(queue: PAYMENT_APPROVED_QUEUE,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var paymentApprovedBytes = eventArgs.Body.ToArray();
                var paymentApprovedJson = Encoding.UTF8.GetString(paymentApprovedBytes);
                var paymentApproved = JsonSerializer.Deserialize<PaymentApprovedIntegrationEvent>(paymentApprovedJson);

                await this.FinishProject(paymentApproved.IdProject);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(PAYMENT_APPROVED_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public async Task FinishProject(int id)
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var unitOfWork = scoped.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var project = await unitOfWork.Projects.GetByIdAsync(id);

                project.Finish();

                await unitOfWork.CompleteAsync();
            }
        }
    }
}
