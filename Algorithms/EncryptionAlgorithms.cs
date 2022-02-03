using System.Text;

namespace CryptographicsAlgorithms.Algorithms
{
	public static class EncryptionAlgorithms
	{
        public static string VigenereCipher(this string input, string key, bool encipher = true)
		{
            var result = new StringBuilder();

			var m = key.Length;
            for (int i = 0; i < input.Length; i++)
            {
				if (!char.IsLetter(input[i]))
				{
                    result.Append(input[i]);
                    continue;
                }

                var isUpper = char.IsUpper(input[i]);
                var characterUpper = char.ToUpper(input[i]);
                var keyUpper = char.ToUpper(key[i % m]);

                var offSet = isUpper ? 'A' : 'a';

                var shifft = encipher ? characterUpper + keyUpper : characterUpper - keyUpper + 26;
                var presetResult = shifft % 26;

                var characterResult = presetResult + offSet;

                result.Append((char)characterResult);
            }

            return result.ToString();
        }
	}
}