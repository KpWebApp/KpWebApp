using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Entities;

namespace BLL
{
    public static class TextParser
    {
        public static IEnumerable<string> ParseString(string text)
        {
            char[] separatingChars = new[] {'\n', '\t'};
            return text.Split(separatingChars);
        }

        public static string TranslatCategoryToUkrainian(string category)
        {
            switch (category)
            {
                case "History":
                {
                    return "Історія";
                    break;
                }
                case "News":
                {
                    return "Новини";
                    break;
                }
                case "InterestingFacts":
                {
                    return "Цікаве";
                    break;
                }
                default:
                {
                    return category;
                }

            }
        }

        public static string TranslatCategoryToEnglish(string category)
        {
            switch (category)
            {
                case "Історія":
                    {
                        return "History";
                        break;
                    }
                case "Новини":
                    {
                        return "News";
                        break;
                    }
                case "Цікаве":
                    {
                        return "InterestingFacts";
                        break;
                    }
                default:
                {
                    return category;
                }

            }
        }
    }
}
