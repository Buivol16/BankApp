using BankApp.exceptions;
using static BankApp.assets.ConsoleHelper;

namespace BankApp.login
{
    class LoginHandler
    {
        private readonly ILoginInterceptor loginInterceptor;

        public LoginHandler(ILoginInterceptor loginInterceptor)
        {
            this.loginInterceptor = loginInterceptor;
        }

        public void Login(out byte[] token)
        {

            WriteLine("Zaloguj się do swojego konta");
            WriteLine("Login: ");
            var login = TypeString();
            WriteLine("Password: ");
            var password = TypeString();
            try
            {
                token = loginInterceptor.DoLogin(login, password);
            }
            catch (BadCredentialsException exceptions)
            {
                WriteLine("Wrong login or password. Try again.");
                token = null;
            }
        }

        public bool Logout(ref byte[] token)
        {
            loginInterceptor.DoLogout(ref token);
            return true;
        }
    }
}
