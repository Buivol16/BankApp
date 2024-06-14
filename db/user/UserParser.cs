using BankApp.db.card;
using BankApp.db.io;

namespace BankApp.db.user
{
    public class UserParser
    {
        private static readonly CardFacade cardFacade = new();

        public static User UserDTOToUser(UserDTO userDTO)
        {
            return new User(
                null,
                userDTO.Login,
                userDTO.Password,
                userDTO.PhoneNumber,
                userDTO.Email);
        }

        public static UserDTO UserToUserDTO(User user)
        {
            var cardDto = CardParser.CardToCardDTO(cardFacade.FindByOwner(user));
            return new UserDTO(user.Login, user.Password, user.Email, user.PhoneNumber, cardDto);
        }
    }
}