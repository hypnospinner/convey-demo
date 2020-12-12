using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Commands
{
    public class FinishParty : ICommand
    {
        public Guid Id { get; set; }
    }

    public class FinishPartyHandler : ICommandHandler<FinishParty>
    {
        private readonly IMongoRepository<Party, Guid> _repository;

        public FinishPartyHandler(IMongoRepository<Party, Guid> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(FinishParty command)
        {
            var party = await _repository.GetAsync(command.Id);

            if (party.Status == PartyStatus.ORDERED)
                throw new Exception($"Cannot finish party {command.Id}");

            party.Status = PartyStatus.FINISHED;

            await _repository.UpdateAsync(party);
        }
    }
}