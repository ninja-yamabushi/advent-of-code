using AdventOfCode.Common;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{
    public class Puzzle4_1 : IPuzzle
    {
        private readonly string hashPrefix;

        public Puzzle4_1() : this("00000") { }
        public Puzzle4_1(string hashPrefix)
        {
            this.hashPrefix = hashPrefix;
        }


        public string Solve(string inputs)
        {
            bool found = false;
            int secret = 0;

            using (MD5 md5Hash = MD5.Create())
            {
                while (!found)
                {
                    string hash = GetMd5Hash(md5Hash, inputs + secret.ToString());
                    if (hash.StartsWith(hashPrefix))
                        found = true;
                    else
                        secret++;

                }
                return secret.ToString();
            }
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }

    public class Puzzle4_2 : Puzzle4_1
    {
        public Puzzle4_2() : base("000000")
        { }
    }
}