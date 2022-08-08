using System;
using System.IO;
namespace SignatureAnaliz
{
    class Program
    {
        static void Main(string[] args)
        {
            string PathToFolder = @"D:\Проверка";
            string Signature = "Turbo Kukac 9.9";
            byte[] SignatureBytes = System.Text.Encoding.UTF8.GetBytes(Signature);
            string[] allfiles = Directory.GetFiles(PathToFolder);
            foreach (string filename in allfiles)
            {
                Console.WriteLine(filename);
                byte[] data = File.ReadAllBytes(filename);
                if (FindSubstring(SignatureBytes, data)==-1) 
                { Console.WriteLine("Не содежит вирусов"); } else
                { Console.WriteLine("Содержит вирус KUKAC.COM"); }
                Console.WriteLine();
            }
        }


        static int[] GetPrefix(byte[] s)
        {
            int[] result = new int[s.Length];
            result[0] = 0;
            int index = 0;

            for (int i = 1; i < s.Length; i++)
            {
                while (index >= 0 && s[index] != s[i]) { index--; }
                index++;
                result[i] = index;
            }

            return result;
        }

        static int FindSubstring(byte[] pattern, byte[] text)
        {
            int[] pf = GetPrefix(pattern);
            int index = 0;

            for (int i = 0; i < text.Length; i++)
            {
                while (index > 0 && pattern[index] != text[i]) { index = pf[index - 1]; }
                if (pattern[index] == text[i]) index++;
                if (index == pattern.Length)
                {
                    return i - index + 1;
                }
            }

            return -1;
        }
    }
}
