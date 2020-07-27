using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using SprintManagement.Application.Commands;
using SprintManagement.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace SprintManagement.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        //private readonly ISprintDetailRepository _repository; // we added this in order to resolve in mediatR

        //public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper, ISprintDetailRepository _repository)
        //{
        //    _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        //    _repository = _repository ?? throw new ArgumentNullException(nameof(_repository));
        //}

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.UpdateIssueInSprintQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.UpdateIssueInSprintQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.UpdateIssueInSprintQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var updateIssueInSprintEvent = JsonConvert.DeserializeObject<UpdateIssueInSprintEvent>(message);

                // EXECUTION : Call Internal Checkout Operation
                var command = _mapper.Map<UpdateIssueInSprintCommand>(updateIssueInSprintEvent);
                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
