using Cipher_labs_1_4;
#region ColumnCipher test

//string message = "первая лабораторная работа по киоки!";
//string key = "криптография";

//Console.WriteLine("Столбцовый метод");
//Console.WriteLine("Начальное: " + message);
//ColumnCipher ciph = new ColumnCipher(message, key);
//ciph.Encrypt();
//Console.WriteLine("Зашифрованное: " + ciph.EncryptedMessage);

//ColumnCipher ciphdecrypt = new ColumnCipher(ciph.EncryptedMessage, ciph.Key);
//ciphdecrypt.Decrypt();
//Console.WriteLine("Расшифрованное: " + ciphdecrypt.DecryptedMessage);

#endregion

#region CesarCipher test

//Console.WriteLine("----------------------------------------------------------------");
//Console.WriteLine("метод Цезаря");

////message = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя!";
//message = "курица! привет";
//int keycesar = 3;

//Console.WriteLine("Начальное: " + message);
//CesarCipher cesciph = new CesarCipher(message, keycesar);
//cesciph.Encrypt();
//Console.WriteLine("Зашифрованное: " + cesciph.EncryptedMessage);

//CesarCipher cesciph2 = new CesarCipher(cesciph.EncryptedMessage, cesciph.Key);
//cesciph2.Decrypt();
//Console.WriteLine("Расшифрованное: " + cesciph2.DecryptedMessage);

//Console.WriteLine();
//Console.WriteLine("END");

#endregion

#region ElGamalCipher test

//while (true)
//{

//        string messageElGamal = "5";
//        Console.WriteLine("Сообщение: " + messageElGamal);
//        ElGamalCipher ch = new ElGamalCipher(messageElGamal);
//        ch.KeyGeneration();
//        Console.WriteLine("Ключ сгенерирован");

//        Console.WriteLine("Секретный ключ: " + ch.KeySecret);
//        Console.WriteLine($"Открытый ключ: p = {ch.Keyopen["p"]}; g = {ch.Keyopen["g"]}; y = {ch.Keyopen["y"]}");
//        ch.Encrypt();
//        Console.WriteLine("Зашифрованное сообщение: " + ch.EncryptedMessage);
//        ch.Message = ch.EncryptedMessage;
//        Console.WriteLine("Расшифрованное сообщение: " + ch.Decrypt());
//}


#endregion

#region EDSRSA test

Console.WriteLine("RSA (lab3)");
EDSRSA rsa = new EDSRSA();
rsa.Check("Hello");

#endregion

#region GuillouQuisquater test

Console.WriteLine("Пример алгоритма Гиллу-Кискатра (лабораторная 4):");
GuillouQuisquater GQ = new GuillouQuisquater();
GQ.Cipher();

#endregion

Console.ReadLine();
