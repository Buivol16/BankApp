using BankApp.assets;
using BankApp.login;
using BankApp.login.impls;
using BankApp.menu;
using BankApp.register;
using BankApp.register.impl;

namespace BankApp
{
    public class BankApp
    {
        private static byte[] token;
        private static readonly RegisterInterceptor registerInterceptor = new();
        private static readonly RegisterHandler registerHandler = new(registerInterceptor);
        private static readonly LoginInterceptor loginInterceptor = new();
        private static readonly LoginHandler loginHandler = new(loginInterceptor);
        private static readonly IMenuInterceptor menuInterceptor = new BankMenuInterceptor();
        private static readonly MenuHandler menuHandler = new(menuInterceptor);

        public static void Main(string[] args)
        {
            Console.WriteLine("Dzień dobry!");
            ChooseAction();
        }

        private static void ChooseAction()
        {
            while (true)
            {
                if (token != null && token.Length != 0)
                {
                    menuHandler.DoMenu(ref token);
                }
                else
                {
                    Console.WriteLine("Wybierz: \n1. Zalogować się\n2. Zarejestrować się");
                    switch (ConsoleHelper.TypeInt())
                    {
                        case 1:
                            Login();
                            break;
                        case 2:
                            Register();
                            break;
                    }
                }
            }
        }

        private static void Login()
        {
            loginHandler.Login(out token);
        }

        private static void Register()
        {
            registerHandler.DoRegister(out token);
        }
    }
}