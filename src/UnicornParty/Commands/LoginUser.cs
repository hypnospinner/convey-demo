using System;
using System.Threading.Tasks;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;
using UnicornParty.Models;

namespace UnicornParty.Commands
{
    public class LoginUser : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class LoginUserHandler : ICommandHandler<LoginUser>
    {
        private readonly IMongoRepository<User, Guid> _repository;
        private readonly IJwtHandler _jwtHandler;

        public LoginUserHandler(IMongoRepository<User, Guid> repository, IJwtHandler jwtHandler)
        {
            _repository = repository;
            _jwtHandler = jwtHandler;
        }

        public async Task HandleAsync(LoginUser command)
        {
            var user = await _repository.GetAsync(x => x.Email == command.Email && x.Password == command.Password);

            if (user == null)
                throw new Exception("User is not valid");

            var token = _jwtHandler.CreateToken(user.Id.ToString());

            command.Token = token.AccessToken;
        }

    }
}