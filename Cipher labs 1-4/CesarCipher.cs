using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_labs_1_4
{
    class CesarCipher : Cipher
    {
        int key;
        Hashtable alfabet = new Hashtable();

        const int alfabetlettercount = 33;

        public CesarCipher(string message = "", int key = 0)
            : base(message)
        {
            this.key = key;

            alfabet = Alfabetcreate();
            KeyRecalculation();
        }

        public int Key
        {
            get { return key; }
            set { key = value; KeyRecalculation(); }
        }

        private Hashtable Alfabetcreate()
        {
            Hashtable alfabetcr = new Hashtable();
            int letternum = 0;
            for (char letter = 'а'; letter <= 'я'; letter++)
            {
                alfabetcr.Add(letter, letternum);
                if (letter == 'е')
                {
                    letternum++;
                    alfabetcr.Add('ё', letternum);
                }
                letternum++;
            }
            return alfabetcr;
        }

        private void KeyRecalculation()
        {
            if (key > 33)
            {
                key -= 33 * (key / 33);
            }
            if (key < 0)
            {
                for (; key < 0; key += 33) { }
            }
        }

        public override string Encrypt()
        {

            EncryptedMessage = "";
            for (int letternum = 0; letternum < Message.Length; letternum++)
            {
                if (alfabet.ContainsKey(Message[letternum]))
                {
                    EncryptedMessage += alfabet.Keys.OfType<char>().Single(a => (int)alfabet[a] == ((int)alfabet[Message[letternum]] + key) % alfabetlettercount);
                }
                else EncryptedMessage += Message[letternum];
            }
            return EncryptedMessage;
        }

        public override string Decrypt()
        {
            DecryptedMessage = "";
            for (int letternum = 0; letternum < Message.Length; letternum++)
            {
                if (alfabet.ContainsKey(Message[letternum]))
                {
                    DecryptedMessage += alfabet.Keys.OfType<char>().Single(a => (int)alfabet[a] == ((int)alfabet[Message[letternum]] + alfabetlettercount - key) % alfabetlettercount);
                }
                else DecryptedMessage += Message[letternum];
            }
            return DecryptedMessage;
        }
    }
}
