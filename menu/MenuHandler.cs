using BankApp.exceptions;
using BankApp.login;
using static BankApp.assets.ConsoleHelper;

namespace BankApp.menu
{
    public class MenuHandler
    {
        private readonly IMenuInterceptor menuInterceptor;

        public MenuHandler(IMenuInterceptor menuInterceptor)
        {
            this.menuInterceptor = menuInterceptor;
        }

        public void DoMenu(ref byte[] token)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Wybierz: \n1. O mnie\n2. O końcie bankowym\n3. Przelew na numer karty\n4. Historia przelewów\n5. Wylogować się");
                    switch (TypeInt())
                    {
                        case 1:
                            menuInterceptor.AboutMe(token);
                            break;
                        case 2:
                            menuInterceptor.BankAccount(token);
                            break;
                        case 3:
                            menuInterceptor.MakeTransaction(token);
                            break;
                        case 4:
                            menuInterceptor.TransactionHistory(token);
                            break;
                        case 5:
                            menuInterceptor.Logout(ref token);
                            throw new LogoutException();
                        default:
                            break;
                    }
                }
            }
            catch (LogoutException) { }
        }
    }
}