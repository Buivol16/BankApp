using BankApp.db.card;
using BankApp.db.user;

namespace BankApp.register
{
    interface IRegisterInterceptor
    {
        public void Register(UserDTO userDTO, out byte[] token);
        public void Logout(out byte[] token);
        public bool CheckLogin(string login);
        public bool CheckEmail(string email);
        public bool CheckPhoneNumber(string phoneNumber);
        public bool CheckCardNumber(int cardNumber);
        public Card GiveFreeCard(User owner);
    }

}