// CascCrypto.Crypto
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Crypto
{
	public const string DefaultIV = "1tdyjCbY1Ix49842";

	public const int Keysize = 128;

	public static string EncryptString(string Plaintext, string Key)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(Plaintext);
		Aes aes = Aes.Create();
		aes.BlockSize = 128;
		aes.KeySize = 128;
		aes.IV = Encoding.UTF8.GetBytes("1tdyjCbY1Ix49842");
		aes.Key = Encoding.UTF8.GetBytes(Key);
		aes.Mode = CipherMode.CBC;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
			{
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
			}
			return Convert.ToBase64String(memoryStream.ToArray());
		}
	}

	public static string DecryptString(string EncryptedString, string Key)
	{
		//Discarded unreachable code: IL_009e
		byte[] array = Convert.FromBase64String(EncryptedString);
		Aes aes = Aes.Create();
		aes.KeySize = 128;
		aes.BlockSize = 128;
		aes.IV = Encoding.UTF8.GetBytes("1tdyjCbY1Ix49842");
		aes.Mode = CipherMode.CBC;
		aes.Key = Encoding.UTF8.GetBytes(Key);
		using (MemoryStream stream = new MemoryStream(array))
		{
			using (CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read))
			{
				byte[] array2 = new byte[checked(array.Length - 1 + 1)];
				cryptoStream.Read(array2, 0, array2.Length);
				return Encoding.UTF8.GetString(array2);
			}
		}
	}
}
