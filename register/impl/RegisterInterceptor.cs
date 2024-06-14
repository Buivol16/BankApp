using BankApp.db.card;
using BankApp.db.io;
using BankApp.db.user;
using BankApp.exceptions;

using static System.Text.Encoding;
using static BankApp.crypts.Cryptographer.AES256;

namespace BankApp.register.impl
{
    class RegisterInterceptor : IRegisterInterceptor

    {
        private readonly UserFacade userFacade = new UserFacade();
        private readonly CardFacade cardFacade = new CardFacade();

        public bool CheckEmail(string email)
        {
            var users = userFacade.FindAll();
            if (users.Length != 0 && users.Where(x => x.Email.Equals(email)).Count() == 1) throw new ObjectExistsException("This email is exists");
            return false;
        }

        public bool CheckCardNumber(int cardNumber)
        {
            var card = cardFacade.FindByCardNumber(cardNumber);
            if (card != null) throw new ObjectExistsException("This card number is exists");
            return false;
        }

        public bool CheckPhoneNumber(string phoneNumber)
        {
            var users = userFacade.FindAll();
            if (users.Length != 0 && users.Where(x => x.PhoneNumber.Equals(phoneNumber)).Count() == 1) throw new ObjectExistsException("This phone number is exists");
            return false;
        }

        public bool CheckLogin(string login)
        {
            var users = userFacade.FindAll();
            if (users.Length != 0 && users.Where(x => x.Login.Equals(login)).Count() == 1) throw new ObjectExistsException("This login is exists");
            return false;
        }

        public Card GiveFreeCard(User user)
        {
            var random = new Random();
            var num = 100_000_000;
            while (true)
            {
                try
                {
                    num = random.Next(100_000_000, 999_999_999);
                    CheckCardNumber(num);
                    continue;
                }
                catch { }
                break;
            }
            return cardFacade.Save(new Card(num, user, 100));
        }

        public void Logout(out byte[] token)
        {
            token = null;
        }

        public void Register(UserDTO userDTO, out byte[] token)
        {
            var key = UTF8.GetBytes("hello");
            var iv = UTF8.GetBytes("world");
            GenerateKeyIV(ref key, ref iv);
            var user = UserParser.UserDTOToUser(userDTO);
            user.Password = UTF8.GetString(EncryptString(plainText: user.Password, key, iv));
            user = userFacade.Save(user);
            GiveFreeCard(user);
            if (user.Id != null) token = EncryptString(plainText: user.ToJson(), key, iv);
            else token = new byte[0];
        }


    }
}