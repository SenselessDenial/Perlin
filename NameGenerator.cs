using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    static class NameGenerator
    {

        static List<string> Vowels = new List<string>()
        {
            "a",
            "e",
            "i",
            "o",
            "u",
            "y"
        };

        static List<string> CompoundPrefix = new List<string>()
        {
            "kirch",
            "jotun",
            "bram",
            "hilder",
            "dorn",
            "allen",
            "carvi",
            "rolf",
            "wurz",
            "volt",
            "ord"
        };

        static List<string> CompoundSuffix = new List<string>()
        {
            "holt",
            "heim",
            "brand",
            "mund",
            "wick",
            "burg",
            "stock",
            "bronn"
        };


        public static string GenerateComboName()
        {
            return Capitalize(RandomPrefix() + RandomSuffix());
        }

        private static string Capitalize(string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        private static string RandomPrefix()
        {
            return Calc.ChooseRandom(CompoundPrefix);
        }

        private static string RandomSuffix()
        {
            return Calc.ChooseRandom(CompoundSuffix);
        }


    }
}
