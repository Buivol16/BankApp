using BankApp.db.card;

namespace BankApp.db.user
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public CardDTO CardDTO{ get; set; }

        public UserDTO(string login, string password, string email, string phoneNumber, CardDTO? cardDTO)
        {
            Login = login;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            this.CardDTO = cardDTO;
        }

        public override string ToString()
        {
            return $"Login: {Login}\nEmail: {Email}\nNumer telefonu: {PhoneNumber}\nNumer karty: {CardDTO.CardNumber}\n";
        }

    }
}