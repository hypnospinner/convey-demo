using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Commands
{
    public class OrderParty : ICommand
    {
        public Guid CustomerId { get; set; }
        public Guid UnicornId { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderPartyHandler : ICommandHandler<OrderParty>
    {
        private readonly IMongoRepository<Party, Guid> _repository;

        public OrderPartyHandler(IMongoRepository<Party, Guid> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(OrderParty command)
        {
            await _repository.AddAsync(new Party()
            {
                CustomerId = command.CustomerId,
                UnicornId = command.UnicornId,
                Address = command.Address,
                Price = command.Price,
                Status = PartyStatus.ORDERED,
            });
        }
    }
}