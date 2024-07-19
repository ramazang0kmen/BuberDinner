using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Query.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authantication;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDinner.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.user);
        }
    }
}
