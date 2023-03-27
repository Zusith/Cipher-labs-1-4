using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_labs_1_4
{
    abstract class Cipher
    {
        string message;
        string encryptedmessage = "";
        string decryptedmessage = "";

        public Cipher(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string EncryptedMessage
        {
            get { return encryptedmessage; }
            protected set { encryptedmessage = value; }
        }

        public string DecryptedMessage
        {
            get { return decryptedmessage; }
            protected set { decryptedmessage = value; }
        }

        public abstract string Encrypt();
        public abstract string Decrypt();
    }
}
