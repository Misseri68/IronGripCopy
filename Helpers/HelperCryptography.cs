using System.Security.Cryptography;
using System.Text;

namespace IronGrip.Helpers
{
    public class HelperCryptography
    {

        private static int SALT_LENGTH = 60;

        public static string GenerateSalt()
        {
            Random random = new Random();
            StringBuilder salt = new StringBuilder();

            for(int i = 0; i < SALT_LENGTH; i++)
            {
                int aleatorio = random.Next(1,255);
                char letra = Convert.ToChar(aleatorio);
                salt.Append(letra);
            }

            return salt.ToString();
        }

        public static bool CompareArrays(byte[] a, byte[] b)
        {
            //if(a.Length == b.Length)
            //{
            //    for(int i = 0; i< a.Length; i++)
            //    {
            //        if (a[i] != b[i])
            //        {
            //            return false;
            //        }
            //    }
            //    return true;
            //}
            //return false;
            return a.SequenceEqual(b);
        }


        public static byte[] EncryptPassword(string password, string salt) 
        {
            string contenido = salt + password;
            SHA512 managed = SHA512.Create();

            byte[] salida = Encoding.UTF8.GetBytes(contenido);

            for(int i = 0; i<20; i++)
            {
                salida = managed.ComputeHash(salida);
            }

            managed.Clear();
            return salida;
        }


    }
}
