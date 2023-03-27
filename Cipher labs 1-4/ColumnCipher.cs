using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_labs_1_4
{
    class ColumnCipher : Cipher
    {
        string key; //ключ для шифрования

        public ColumnCipher(string message = "", string key = "") //конструктор класса солбцового шифра
            : base(message) //передаваемое сообщение для шифровки / дешифровки
        {
            this.key = key;
            KeyRecalculation();
        }

        public string Key //Свойство ключ, пользователь может менять значение ключа
        {
            get { return key; }
            set { key = value; KeyRecalculation(); }
        }

        private void KeyRecalculation() //перерасчет ключа, если он состоит из нескольких слов
        {
            if (key.Contains(" "))
            {
                string[] test = key.Split(' ');
                key = "";
                for (int num = 0; num < test.Length; num++)
                {
                    key += test[num];
                }
            }
        }

        private string[,] CipherTableCreate() //Заполнение ключа и расчет нумеровки букв в таблице
        {
            int rowscount = 0; //количетсво строк
            rowscount = (int)Math.Ceiling((double)Message.Length / (double)key.Length);

            string[,] ciphertable = new string[2 + rowscount, key.Length];//таблица для шифровки
            //Заполнение первой строки таблицы ключом
            for (int column = 0; column < ciphertable.GetLength(1); column++)
            {
                ciphertable[0, column] = Convert.ToString(key[column]);
            }

            int countletterkey = 1; //количество букв в ключе
            for (int letter = 'а'; letter < 'я' + 1; letter++) // прогонка по всем буквам
            {
                for (int column = 0; column < ciphertable.GetLength(1); column++)
                {
                    if (key[column] == letter) //нумеровка столбцов в алфавитном порядке
                    {
                        ciphertable[1, column] = Convert.ToString(countletterkey);
                        countletterkey++;
                    }
                }
            }

            return ciphertable;
        }

        public override string Encrypt() //зашифровка сообщения солбцовым методом
        {
            string[,] ciphertable = CipherTableCreate(); //Создание таблицы для зашифровки
            int countlettermess = 0; //количетство букв в сообщении
            bool checkletters = false; //проверка количества букв
            for (int row = 2; row < ciphertable.GetLength(0); row++) //по строкам
            {
                for (int column = 0; column < ciphertable.GetLength(1); column++) //по столбцам
                {
                    if (countlettermess == Message.Length) //проверка на конец сообщения, чтобы не заполнялись пустые клетки
                    {
                        checkletters = true;
                        break;
                    }
                    ciphertable[row, column] = Convert.ToString(Message[countlettermess]);
                    countlettermess++;
                }
                if (checkletters) //проверка на заполненность, сообщение закончено
                {
                    break;
                }
            }

            EncryptedMessage = ""; //Зашифровка
            //Прогонка по нумерации букв ключа
            for (int countnumbers = 1; countnumbers != Key.Length + 1; countnumbers++)
            {   //по столбцам
                for (int column = 0; column < ciphertable.GetLength(1); column++)
                {   //проверка на соответствие: номер в выбранном столбце - текущая нумерация ключа
                    if (ciphertable[1, column] == Convert.ToString(countnumbers))
                    {   //по строкам
                        for (int row = 2; row < ciphertable.GetLength(0); row++)
                        {
                            EncryptedMessage += ciphertable[row, column];
                        }
                        break;
                    }
                }
            }

            return EncryptedMessage;
        }

        public override string Decrypt() //расшифровка сообщения, зашифрованного столбцовым методом
        {
            string[,] ciphertable = CipherTableCreate(); //создание таблицы
            //количество пустых клеток в конце таблицы
            int countemptycells = (ciphertable.GetLength(0) - 2) * ciphertable.GetLength(1) - Message.Length;

            int lettermessnum = 0; //кол-во букв в сообщении
            //прогонка по нумерации букв ключа
            for (int letterkey = 1; letterkey < key.Length + 1; letterkey++)
            {   //по столбцам
                for (int column = 0; column < ciphertable.GetLength(1); column++)
                {   //проверка на соответствие: выбранная нумерация столбца - текущая нумерация букв ключа
                    if (ciphertable[1, column] == Convert.ToString(letterkey))
                    {   //по строкам
                        for (int row = 2; row < ciphertable.GetLength(0); row++)
                        {   //проверка должна ли быть в этом столбце пустая клетка
                            if (row == ciphertable.GetLength(0) - 1 && column > ciphertable.GetLength(1) - countemptycells - 1)
                            {
                                break;
                            }
                            ciphertable[row, column] = Convert.ToString(Message[lettermessnum]);
                            lettermessnum++;
                        }
                        break;
                    }
                }
            }

            DecryptedMessage = ""; //Расшифровка сообщения
            //по строкам
            for (int row = 2; row < ciphertable.GetLength(0); row++)
            {   //по столбцам
                for (int column = 0; column < ciphertable.GetLength(1); column++)
                {
                    DecryptedMessage += ciphertable[row, column];
                }
            }


            return DecryptedMessage;
        }
    }
}
