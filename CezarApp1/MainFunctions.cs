using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CezarApp1
{
    struct TextVariables
    {
        public string textVar;
        public int keyVar;
    };
    internal class MainFunctions
    {
        public static string Encrypt(string input, int key)
        {
            StringBuilder encryptedText = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char encryptedChar = (char)(((c - 'a' + key) % 26) + 'a');
                    encryptedText.Append(encryptedChar);
                }
                else
                {
                    encryptedText.Append(c);
                }
            }
            return encryptedText.ToString();
        }

        public static string Decrypt(string input, int key)
        {
            StringBuilder decryptedText = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char decryptedChar = (char)(((c - 'a' - key + 26) % 26) + 'a');
                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    decryptedText.Append(c);
                }
            }
            return decryptedText.ToString();
        }

        public static int DetectKey(string plaintext, string encryptedText)
        {
            int key = 0;
            string decryptedText = "";
            do
            {
                decryptedText = Decrypt(encryptedText, key);
                key++;
            } while (decryptedText != plaintext && key < 26);

            return decryptedText == plaintext ? key - 1 : -1;
        }

        public static async Task<TextVariables[]> Attack(string encryptedText)
        {
            List<TextVariables> potentialSolutions = new List<TextVariables>();
            for (int key = 0; key < 26; key++)
            {
                string decryptedText = Decrypt(encryptedText, key);
                var searchResults = await APIIntegration.SearchLatinWord(decryptedText);

                if (searchResults.Count > 0)
                {
                    potentialSolutions.Add(new TextVariables { textVar = decryptedText, keyVar = key });
                }
                /*else
                {
                    potentialSolutions.Add(new TextVariables { textVar = decryptedText + " (No Latin words found)", keyVar = key });
                }*/
            }

            return potentialSolutions.ToArray();
        }
    }
}
