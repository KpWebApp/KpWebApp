using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Entities;

namespace KPWebApp.BLL
{
    public static class TextParser
    {
        public static IEnumerable<string> ParseString(string text)
        {
            char[] separatingChars = new[] { '\n', '\t' };
            return text.Split(separatingChars);
        }

        public static string TranslateCategoryToUkrainian(string category)
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
                case "Male":
                    {
                        return "Чоловіча";
                        break;
                    }
                case "Female":
                    {
                        return "Жіноча";
                        break;
                    }
                case "Programming":
                    {
                        return "Програмування";
                        break;
                    }
                case "InformationSystems":
                    {
                        return "Інформаційних систем";
                        break;
                    }
                case "DescreteAnalysys":
                    {
                        return "Дискретного аналізу та інтелектуальних систем";
                        break;
                    }
                case "NotSpecified":
                    {
                        return "Не вказано";
                        break;
                    }
                default:
                    {
                        return category;
                    }

            }
        }

        public static string TranslateCategoryToEnglish(string category)
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
                case "Чоловіча":
                    {
                        return "Male";
                        break;
                    }
                case "Жіноча":
                    {
                        return "Female";
                        break; 
                    }
                case "Програмування":
                    {
                        return "Programming";
                        break;
                    }
                case "Інформаційних систем":
                    {
                        return "InformationSystems";
                        break;
                    }
                case "Дискретного аналізу та інтелектуальних систем":
                    {
                        return "DescreteAnalysys";
                        break;
                    }
                case "Не вказано":
                    {
                        return "NotSpecified";
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
