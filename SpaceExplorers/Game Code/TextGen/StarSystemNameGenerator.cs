using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnoleEngine.Engine_Base.Game_Code.TextGen
{
    class StarSystemNameGenerator
    {
        public static string GenerateStarSystemName()
        {
            Random objRandom = new Random();
            Random rnd = new Random();
            List<string> consonants = new List<string>() { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
            List<string> commonConsonants = new List<string>() { "b", "c", "d", "f", "g", "h", "k", "l", "m", "n", "p", "r", "s", "t" };
            List<string> rareConsonants = new List<string>() { "q", "j", "v", "w", "x", "y", "z" };
            List<string> vowels = new List<string>() { "a", "e", "i", "o", "u", "y" };

            List<string> qFragments = new List<string>() { "que", "qua", "quo", "qui" };
            List<string> zFragments = new List<string>() { "z", "ze", "za", "zo", "zoo", "zee", "zoe" };
            List<string> xFragments = new List<string>() { "x", "xa", "xee", "xe", "xi" };
            List<string> yFragments = new List<string>() { "y", "yo", "ye", "ay", "ey", "oy", "ya" };
            List<string> wFragments = new List<string>() { "w", "wo", "we", "wa", "wi", "wie", "woe", "woo", "wee", "wea", "woa" };
            List<string> jFragments = new List<string>() { "j", "jo", "je", "ji", "ja", "ju" };
            List<string> kFragments = new List<string>() { "k", "k", "k", "ko", "ke", "ki", "ka", "ku" };
            List<string> vFragments = new List<string>() { "v", "vo", "ve", "vi", "va", "vu" };
            List<string> hFragments = new List<string>() { "h", "h", "h", "ho", "he", "hi", "ha", "hu" };

            List<string> oFragments = new List<string>() { "o", "ou", "oi", "oa", "oo" };
            List<string> eFragments = new List<string>() { "e", "ea", "ei" };
            List<string> aFragments = new List<string>() { "a", "au" };
            List<string> prefixFragments = new List<string>() { "Pax ", "Proxima ", "Minas ", "CX ", "CY ", "AX ", "Centaurus ", "Canus ", "Ursa ", "Republic of ", "United Federation of " };
            List<string> suffixFragments = new List<string>() { " Centari", " Onias", " Zed", " Major", " Minor", " Pax", " Dominion", " Empire", " Imperium", " Commonwealth", " Republic", " Consortium", " Federation", " Union", " Confederacy", " Pile", " Gaggle", " Flock", " Gathering" };
            List<string> greekFragments = new List<string>() { "Alpha", "Beta", "Gamma", "Delta", "Omega", "Sigma", "Xi", "Zeta", "Eta", "Mu", "Phi", "Theta", "Epsilon", "Nu", "Rho", "Tau", "Psi", "Kappa", "Iota", "Lambda", "Upsilon" };

            string starSystemName = "";
            int requestedLength = objRandom.Next(4, 10);
            int intVowelCount = 0;

            while (starSystemName.Length < requestedLength)
            {
                if (requestedLength != 1)
                {
                    double probability = objRandom.NextDouble();

                    if (starSystemName.Length > 0)
                    {
                        string strNextLetter = "";
                        string strLastLetter = "";
                        string strSecondToLastLetter = "";

                        if (starSystemName.Length >= 1)
                        {
                            strLastLetter = starSystemName[starSystemName.Length - 1].ToString();
                        }
                        if (starSystemName.Length >= 2)
                        {
                            strSecondToLastLetter = starSystemName[starSystemName.Length - 2].ToString();
                        }


                        if (probability <= 0.40 && intVowelCount < requestedLength / 4)
                        {
                            if (!vowels.Contains(strLastLetter) && !vowels.Contains(strSecondToLastLetter))
                            {
                                strNextLetter = GetRandomLetter(rnd, vowels);

                                while (strNextLetter.Length == 2 && starSystemName.EndsWith(strNextLetter))
                                {
                                    strNextLetter = GetRandomLetter(rnd, vowels);
                                }

                                intVowelCount++;
                            }
                            else
                            {
                                double consonantProbability = objRandom.NextDouble();

                                if (consonantProbability <= 0.35)
                                {
                                    strNextLetter = GetRandomLetter(rnd, rareConsonants);
                                }
                                else
                                {
                                    strNextLetter = GetRandomLetter(rnd, commonConsonants);
                                }
                            }
                        }
                        else
                        {
                            if (!consonants.Contains(strLastLetter) && !consonants.Contains(strSecondToLastLetter))
                            {
                                double consonantProbability = objRandom.NextDouble();

                                if (consonantProbability <= 0.35)
                                {
                                    strNextLetter = GetRandomLetter(rnd, rareConsonants);
                                }
                                else
                                {
                                    strNextLetter = GetRandomLetter(rnd, commonConsonants);
                                }
                            }
                            else
                            {
                                strNextLetter = GetRandomLetter(rnd, vowels);

                                while (strNextLetter.Length == 2 && starSystemName.EndsWith(strNextLetter))
                                {
                                    strNextLetter = GetRandomLetter(rnd, vowels);
                                }

                                intVowelCount++;
                            }
                        }

                        if (strNextLetter == "q")
                        {
                            strNextLetter = GetRandomLetter(rnd, qFragments);
                        }
                        else if (strNextLetter == "z")
                        {
                            strNextLetter = GetRandomLetter(rnd, zFragments);
                        }
                        else if (strNextLetter == "x")
                        {
                            strNextLetter = GetRandomLetter(rnd, xFragments);
                        }
                        else if (strNextLetter == "y")
                        {
                            strNextLetter = GetRandomLetter(rnd, yFragments);
                        }
                        else if (strNextLetter == "w")
                        {
                            strNextLetter = GetRandomLetter(rnd, wFragments);
                        }
                        else if (strNextLetter == "j")
                        {
                            strNextLetter = GetRandomLetter(rnd, jFragments);
                        }
                        else if (strNextLetter == "k")
                        {
                            strNextLetter = GetRandomLetter(rnd, kFragments);
                        }
                        else if (strNextLetter == "v")
                        {
                            strNextLetter = GetRandomLetter(rnd, vFragments);
                        }
                        else if (strNextLetter == "h")
                        {
                            strNextLetter = GetRandomLetter(rnd, hFragments);
                        }
                        else if (strNextLetter == "e")
                        {
                            strNextLetter = GetRandomLetter(rnd, eFragments);
                        }
                        else if (strNextLetter == "o")
                        {
                            strNextLetter = GetRandomLetter(rnd, oFragments);
                        }
                        else if (strNextLetter == "a")
                        {
                            strNextLetter = GetRandomLetter(rnd, aFragments);
                        }

                        if (!string.IsNullOrEmpty(strNextLetter))
                        {
                            if (consonants.Contains(strLastLetter) && consonants.Contains(strSecondToLastLetter) && consonants.Contains(strNextLetter[0].ToString()))
                            {
                                strNextLetter = GetRandomLetter(rnd, vowels);

                                while (strNextLetter.Length == 2 && ((starSystemName.EndsWith(strNextLetter) || consonants.Contains(strSecondToLastLetter))))
                                {
                                    strNextLetter = GetRandomLetter(rnd, vowels);
                                }

                                intVowelCount++;
                            }
                            if (vowels.Contains(strLastLetter) && vowels.Contains(strSecondToLastLetter) && vowels.Contains(strNextLetter[0].ToString()))
                            {
                                double consonantProbability = objRandom.NextDouble();

                                if (consonantProbability <= 0.3)
                                {
                                    strNextLetter = GetRandomLetter(rnd, rareConsonants);
                                }
                                else
                                {
                                    strNextLetter = GetRandomLetter(rnd, commonConsonants);
                                }
                            }

                            starSystemName += strNextLetter;
                        }
                    }
                    else
                    {
                        if (probability <= 0.5)
                        {
                            starSystemName += GetRandomLetter(rnd, vowels);
                        }
                        else
                        {
                            starSystemName += GetRandomLetter(rnd, commonConsonants);
                        }
                    }
                }

                starSystemName = starSystemName[0].ToString().ToUpper() + starSystemName.Substring(1);

                if (starSystemName.Length < 7)
                {
                    double prefixProbability = objRandom.NextDouble();

                    if (prefixProbability <= 0.15)
                    {
                        string strPrefix = "";
                        double greekPrefixProbability = objRandom.NextDouble();

                        if (greekPrefixProbability <= 0.4)
                        {
                            strPrefix = GetRandomLetter(rnd, greekFragments) + " ";
                        }
                        else
                        {
                            strPrefix = GetRandomLetter(rnd, prefixFragments);
                        }

                        starSystemName = strPrefix + starSystemName;
                    }
                    else if (prefixProbability > 0.8)
                    {
                        string strSuffix = "";
                        double greekSuffixProbability = objRandom.NextDouble();

                        if (greekSuffixProbability <= 0.35)
                        {
                            strSuffix = " " + GetRandomLetter(rnd, greekFragments);
                        }
                        else
                        {
                            strSuffix = GetRandomLetter(rnd, suffixFragments);
                        }

                        starSystemName += strSuffix;
                    }
                }
                else
                {
                    double randomWordProbability = objRandom.NextDouble();

                    if (randomWordProbability <= 0.1)
                    {
                        starSystemName = GetRandomLetter(rnd, greekFragments) + GetRandomLetter(rnd, suffixFragments);
                    }
                    else
                    {
                        randomWordProbability = objRandom.NextDouble();

                        if (randomWordProbability <= 0.1)
                        {
                            starSystemName = GetRandomLetter(rnd, prefixFragments) + GetRandomLetter(rnd, greekFragments);
                        }
                    }
                }


            }

            return starSystemName;
        }

        private static string GetRandomLetter(Random rnd, List<string> letters)
        {
            return letters[rnd.Next(0, letters.Count - 1)];
        }
    }
}
