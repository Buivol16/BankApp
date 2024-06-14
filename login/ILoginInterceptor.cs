namespace BankApp.login
{
    interface ILoginInterceptor
    {
        public byte[] DoLogin(string login, string password);
        public bool DoLogout(ref byte[] token);

    }

}
