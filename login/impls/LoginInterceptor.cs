using System.Text;
using BankApp.db.io;
using BankApp.exceptions;
using static System.Text.Encoding;

namespace BankApp.login.impls
{
    class LoginInterceptor : ILoginInterceptor
    {
        private readonly ITokenGenerator tokenGenerator = new TokenGeneratorImpl();
        private readonly UserFacade userFacade = new();

        public byte[] DoLogin(string login, string password)
        {
            try
            {
                var found = userFacade.FindByLogin(login);
                var passBytes = UTF8.GetBytes(password);
                var foundBytes = UTF8.GetBytes(found.ToString());
                tokenGenerator.GenerateToken(ref passBytes);
                if (found.Password.Equals(passBytes)) tokenGenerator.GenerateToken(ref foundBytes);
                return foundBytes;
            }
            catch { }
            throw new BadCredentialsException("Wrong login or password");

        }

        public bool DoLogout(ref byte[] token)
        {
            token = null;
            return true;
        }
    }
}
