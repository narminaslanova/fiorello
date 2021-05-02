using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Helpers
{
    public class RuIdentityErrorDescriber:IdentityErrorDescriber
    {
        //all methods in IdentityErrorDescriber are virtual and we can override them
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code=nameof(PasswordRequiresLower),
                Description="Пароль должен содержать строчные буквы."
            };
        }
    }
}
