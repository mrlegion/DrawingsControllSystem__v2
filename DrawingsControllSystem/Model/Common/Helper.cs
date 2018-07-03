using System;
using System.Collections.Generic;

namespace DrawingsControllSystem.Model.Common
{
    public static class Helper
    {
        private static readonly Random random = new Random();

        // Todo: Сделать более умную генерацию имени
        public static string GetRandomPrefix(this string name, int length = 5, RandomPrefixType type = RandomPrefixType.Random)
        {

            List<char> all = GetRandomArray(type);

            name += "__";

            for (int i = 0; i < length; i++)
            {
                name += all[random.Next(0, all.Count - 1)];
            }

            return name;
        }

        private static List<char> GetRandomArray(RandomPrefixType type)
        {
            char[] low = new[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                'w', 'x', 'y', 'z',
            };

            char[] up = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
                'W', 'X', 'Y', 'Z',
            };

            char[] numbers = new[]
            {
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            };


            List<char> all = new List<char>();

            switch (type)
            {
                case RandomPrefixType.LowcaseAndNumbers:
                    all.AddRange(low);
                    all.AddRange(numbers);
                    break;
                case RandomPrefixType.OnlyLowcase:
                    all.AddRange(low);
                    break;
                case RandomPrefixType.OnlyNumbers:
                    all.AddRange(numbers);
                    break;
                case RandomPrefixType.OnlyUppercase:
                    all.AddRange(up);
                    break;
                case RandomPrefixType.UppercaseAndNumbers:
                    all.AddRange(up);
                    all.AddRange(numbers);
                    break;
                case RandomPrefixType.UpperAndLowcase:
                    all.AddRange(low);
                    all.AddRange(up);
                    break;
                case RandomPrefixType.Random:
                default:
                    all.AddRange(low);
                    all.AddRange(up);
                    all.AddRange(numbers);
                    break;
            }

            return all;
        }
    }
}