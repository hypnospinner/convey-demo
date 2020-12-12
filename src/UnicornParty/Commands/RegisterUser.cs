using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Commands
{
    public class RegisterUser : User, ICommand { }

    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IMongoRepository<User, Guid> _repository;

        public RegisterUserHandler(IMongoRepository<User, Guid> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            await _repository.AddAsync(command);
        }
    }
}