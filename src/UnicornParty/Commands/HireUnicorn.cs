using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Commands
{
    public class HireUnicorn : Unicorn, ICommand { }

    public class HireUnicornHandler : ICommandHandler<HireUnicorn>
    {
        private readonly IMongoRepository<Unicorn, Guid> _repository;

        public HireUnicornHandler(IMongoRepository<Unicorn, Guid> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(HireUnicorn command)
        {
            await _repository.AddAsync(command);
        }
    }
}