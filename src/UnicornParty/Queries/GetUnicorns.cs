using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Queries
{
    public class GetUnicorns : IQuery<IEnumerable<Unicorn>> { }

    public class GetUnicornsHandler : IQueryHandler<GetUnicorns, IEnumerable<Unicorn>>
    {
        private readonly IMongoRepository<Unicorn, Guid> _repository;

        public GetUnicornsHandler(IMongoRepository<Unicorn, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Unicorn>> HandleAsync(GetUnicorns query)
        {
            return await _repository.FindAsync(x => true);
        }
    }
}