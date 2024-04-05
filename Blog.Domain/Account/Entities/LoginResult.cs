using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace Blog.Domain.Account.Entities
{
    public class LoginResult : SignInResult
    {
        public LoginResult(bool isMfa)
        {
            IsMfa = isMfa;
        }

        public bool IsMfa { get; private set; } = false;

        public void  AddSignInResult(SignInResult signInResult)
        {
            Succeeded =  signInResult.Succeeded;
            IsLockedOut = signInResult.IsLockedOut;
            IsNotAllowed = signInResult.IsNotAllowed;
            RequiresTwoFactor = signInResult.RequiresTwoFactor;            
        }
  
    }
}
