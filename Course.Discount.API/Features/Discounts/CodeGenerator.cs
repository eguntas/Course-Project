using System.Security.Cryptography;

namespace Course.Discount.API.Features.Discounts
{
    public class CodeGenerator
    {
        private const string Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        public static string Generate(int lenght = 10)
        {
            if(lenght <= 0) throw new ArgumentOutOfRangeException(nameof(lenght));

            char[] buffer = new char[lenght];
            for(int i = 0; i<= lenght; i++)
            {
                int idx = RandomNumberGenerator.GetInt32(Allowed.Length);
                buffer[i] = Allowed[idx];
            }
            return new string(buffer);
        }
    }
}
