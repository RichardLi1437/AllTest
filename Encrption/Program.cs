using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrption
{
    class Program
    {
        static void Main(string[] args)
        {
            // The message to encrypt.
            string Msg = "dmsystem@microsoft.com";
            string Password = "dmsystem@";

            string EncryptedString = Encryptor.Encrypt(Msg, Password);
            string DecryptedString = Encryptor.Decrypt(EncryptedString, Password);

            Console.WriteLine("Message: {0}", Msg);
            Console.WriteLine("Password: {0}", Password);
            Console.WriteLine("Encrypted string: {0}", EncryptedString);
            Console.WriteLine("Decrypted string: {0}", DecryptedString);
        }
    }
}
