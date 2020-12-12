using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Queries
{
    public class GetUser : IQuery<User>
    {
        public Guid Id { get; set; }
    }

    public class GetUserHandler : IQueryHandler<GetUser, User>
    {
        private readonly IMongoRepository<User, Guid> _repository;

        public GetUserHandler(IMongoRepository<User, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<User> HandleAsync(Queries.GetUser query)
        {
            return await _repository.GetAsync(query.Id);
        }
    }
}