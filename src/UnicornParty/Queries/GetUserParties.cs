using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Queries
{
    public class GetUserParties : IQuery<IEnumerable<Party>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserPartiesHandler : IQueryHandler<GetUserParties, IEnumerable<Party>>
    {
        private readonly IMongoRepository<Party, Guid> _repository;

        public GetUserPartiesHandler(IMongoRepository<Party, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Party>> HandleAsync(GetUserParties query)
        {
            return await _repository.FindAsync(x => x.CustomerId == query.Id);
        }
    }
}