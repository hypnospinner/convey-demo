using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Commands
{
    public class PayParty : ICommand
    {
        public Guid Id { get; set; }
    }

    public class PayPartyHandler : ICommandHandler<PayParty>
    {
        private readonly IMongoRepository<Party, Guid> _repository;

        public PayPartyHandler(IMongoRepository<Party, Guid> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(PayParty command)
        {
            var party = await _repository.GetAsync(command.Id);

            if (party == null)
                throw new Exception("Cannot pay for non-existing party");

            if (party.Status != PartyStatus.ORDERED)
                throw new Exception("Cannot pay for this party (already payed or finished)");

            party.Status = PartyStatus.PAYED;

            await _repository.UpdateAsync(party);
        }
    }
}