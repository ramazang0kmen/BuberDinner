﻿using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Behaviors
{
    public class ValidationRegisterCommandBehavior : 
        IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IValidator<RegisterCommand> _validator;

        public ValidationRegisterCommandBehavior(IValidator<RegisterCommand> validator)
        {
            _validator = validator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(
            RegisterCommand request, 
            RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next, 
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            var errors = validationResult.Errors
                .ConvertAll(validationFailure => Error.Validation(
                    validationFailure.PropertyName, 
                    validationFailure.ErrorMessage))
                .ToList();
            return errors;
        }
    }
}
