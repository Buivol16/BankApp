using BankApp.db.card;
using BankApp.db.user;
using static BankApp.assets.ConsoleHelper;

namespace BankApp.register
{
    class RegisterHandler
    {
        private IRegisterInterceptor registerInterceptor;

        public RegisterHandler(IRegisterInterceptor registerInterceptor)
        {
            this.registerInterceptor = registerInterceptor;
        }

        public void DoRegister(out byte[] token)
        {
            var i = 0;

            string login = "";
            string password = "";
            string email = "";
            string phoneNumber = "";

            WriteLine("Utwórz nowe konto");
            while (true)
            {
                try
                {
                    switch (i)
                    {
                        case 0:
                            WriteLine("Login:");
                            login = TypeString();
                            registerInterceptor.CheckLogin(login);
                            i = 1;
                            continue;
                        case 1:
                            WriteLine("Password:");
                            password = TypeString();
                            i = 2;
                            continue;
                        case 2:
                            WriteLine("Email:");
                            email = TypeString();
                            registerInterceptor.CheckEmail(email);
                            i = 3;
                            continue;
                        case 3:
                            WriteLine("Phone number:");
                            phoneNumber = TypeString();
                            registerInterceptor.CheckPhoneNumber(phoneNumber);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    continue;
                }
                break;
            }

            registerInterceptor.Register(new UserDTO(login, password, email, phoneNumber, null), out token);
        }
    }

}