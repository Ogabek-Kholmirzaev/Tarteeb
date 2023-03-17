﻿//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using Tarteeb.Api.Models.Orchestrations.UserTokens;
using Tarteeb.Api.Models.Orchestrations.UserTokens.Exceptions;
using Xeptions;

namespace Tarteeb.Api.Services.Orchestrations
{
    public partial class UserSecurityOrchestrationService
    {
        private delegate UserToken ReturningUserTokenFunction();

        private UserToken TryCatch(ReturningUserTokenFunction returningUserTokenFunction)
        {
            try
            {
                return returningUserTokenFunction();
            }
            catch (InvalidUserCredentialOrchestrationException invalidUserCreadentialOrchestrationException)
            {
                throw CreateAndLogValidationException(invalidUserCreadentialOrchestrationException);
            }
            catch (NotFoundUserException notFoundUserException)
            {
                throw CreateAndLogValidationException(notFoundUserException);
            }
        }

        private UserTokenOrchestrationValidationException CreateAndLogValidationException(Xeption exception)
        {
            var userTokenOrchestrationValidationException = new UserTokenOrchestrationValidationException(exception);
            this.loggingBroker.LogError(userTokenOrchestrationValidationException);

            return userTokenOrchestrationValidationException;
        }
    }
}
