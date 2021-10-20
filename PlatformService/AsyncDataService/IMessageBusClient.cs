using PlatformService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.AsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto platform);
    }
}
