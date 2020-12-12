using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UnicornParty.Models;

namespace UnicornParty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                    services
                        .AddConvey()
                        .AddWebApi()
                        // add database with collection for domain entities
                        .AddMongo()
                        .AddMongoRepository<User, Guid>("user")
                        .AddMongoRepository<Party, Guid>("party")
                        .AddMongoRepository<Unicorn, Guid>("unicorn")
                        // add handlers for commands, queries and events
                        .AddCommandHandlers()
                        .AddQueryHandlers()
                        .AddEventHandlers()
                        .AddInMemoryCommandDispatcher()
                        .AddInMemoryQueryDispatcher()
                        .AddInMemoryEventDispatcher()
                        // adding jwt token generation
                        .AddJwt()
                )
                .Configure(app => app
                    .UseConvey()
                    .UseAuthentication()
                    .AddUnicornRoutes());
    }
}
