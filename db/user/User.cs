using System.Text.Json;

namespace BankApp.db.user
{
    public class User
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public User(int? id,
                    string login,
                    string password,
                    string phoneNumber,
                    string email)
        {
            Id = id;
            Login = login;
            Password = password;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public User()
        {
        }

        public string ToJson()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return JsonSerializer.Serialize(this, GetType(), options: jso);
        }

        public override bool Equals(object? obj)
        {
            var eq = obj as User;
            if (Id == eq.Id && Login.Equals(eq.Login) && Password.Equals(eq.Password) && PhoneNumber.Equals(eq.PhoneNumber) && Email.Equals(eq.Email)) return true;
            return false;
        }
    }


}
