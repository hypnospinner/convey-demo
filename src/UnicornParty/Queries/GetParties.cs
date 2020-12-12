using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Queries
{
    public class GetParties : IQuery<IEnumerable<Party>> { }

    public class GetPartiesHandler : IQueryHandler<GetParties, IEnumerable<Party>>
    {
        private readonly IMongoRepository<Party, Guid> _repository;

        public GetPartiesHandler(IMongoRepository<Party, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Party>> HandleAsync(GetParties query)
        {
            return await _repository.FindAsync(x => true);
        }
    }
}