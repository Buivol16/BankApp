using System.Globalization;

namespace BankApp.assets
{
    static class ConsoleHelper
    {
        public static int TypeInt()
        {
            string input = TypeString();
            int number;
            bool isValid = int.TryParse(input, out number);

            if (!isValid)
            {
                Console.WriteLine("Błąd! Wprowadź w poprawnym formacie.");
                return TypeInt();
            }

            return number;
        }

        public static string TypeString()
        {
            return Console.ReadLine();
        }

        public static float TypeFloat()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US");
            string input = TypeString();
            float number;
            if (float.TryParse(input, ci, out number))
            {
                number = float.Parse(input, ci);
                return number;

            }
            else
            {
                Console.WriteLine("Błąd! Wprowadź w poprawnym formacie.");
                return TypeFloat();
            }

        }

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}