using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommandsService.EventProcessing
{
    enum EventType
    {
        PlatformPublished,
        Undetermined
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                case EventType.Undetermined:
                    break;
                default:
                    break;
            }

        }

        private EventType DetermineEvent(string notificationMessage)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            return eventType.Event switch
            {
                "Platform_Published" => EventType.PlatformPublished,
                _ => EventType.Undetermined,
            };
        }

        private async void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishedDto);
                    if (await repo.ExternalPlatformIdExistAsync(platform.ExternalId))
                    {

                    }
                    else
                    {
                        Console.WriteLine("Platform already exist");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
