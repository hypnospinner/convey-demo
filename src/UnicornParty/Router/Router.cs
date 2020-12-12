using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using UnicornParty.Commands;
using UnicornParty.Models;
using UnicornParty.Queries;
using UnicornParty.Utils;

namespace UnicornParty
{
    public static class RouterExtensions
    {
        public static IApplicationBuilder AddUnicornRoutes(this IApplicationBuilder app)
        {
            return app
                .UseDispatcherEndpoints(endpoints => endpoints
                    .Post<FinishParty>(
                        path: "/party/finish",
                        auth: true)
                    .Post<LoginUser>(
                        path: "/login",
                        afterDispatch: (command, context) => context.Response.Ok(command.Token))
                    .Post<RegisterUser>(
                        path: "/register")
                    .Post<OrderParty>(
                        path: "/party/order",
                        beforeDispatch: (command, context) =>
                        {
                            var id = context.GetUserId();
                            command.CustomerId = id;

                            return Task.CompletedTask;
                        },
                        auth: true)
                    .Post<PayParty>(
                        path: "/party/pay",
                        auth: true)
                    .Post<HireUnicorn>(
                        path: "/unicorn/hire",
                        auth: true)
                    .Get<GetParties, IEnumerable<Party>>(
                        path: "/party/get",
                        auth: true)
                    .Get<GetUnicorns, IEnumerable<Unicorn>>(
                        path: "/unicorn/get",
                        auth: true)
                    .Get<GetUserParties, IEnumerable<Party>>(
                        path: "/party/get_user",
                        beforeDispatch: (query, context) =>
                        {
                            var id = context.GetUserId();
                            query.Id = id;

                            return Task.CompletedTask;
                        },
                        auth: true)
                    .Get<GetUser, User>(
                        path: "/user/get",
                        beforeDispatch: (query, context) =>
                        {
                            var id = context.GetUserId();
                            query.Id = id;

                            return Task.CompletedTask;
                        },
                        auth: true));
        }
    }
}