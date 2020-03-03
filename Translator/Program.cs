using Ninject;
using Ninject.Parameters;
using System;

namespace Translator
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel(new TranslatorConfigModule());
            Translator translator = ninjectKernel.Get<Translator>(
                new ConstructorArgument("input", LanguageList.English), 
                new ConstructorArgument("output", LanguageList.Russian));

            Console.WriteLine("Write to change languages:");
            Console.WriteLine("0 - English-Russian. 1 - Russian-English.");
            Console.WriteLine("\nPowered by Yandex.Translate\nhttp://translate.yandex.com\n");
            Console.WriteLine("Enter text:");
            for ( ; ; )
            {
                string text = Console.ReadLine();
                switch (text)
                {
                    case "0":
                        translator.ChangeLanguages(LanguageList.English, LanguageList.Russian);
                        Console.WriteLine("Languages Changed: English-russian");
                        break;
                    case "1":
                        translator.ChangeLanguages(LanguageList.Russian, LanguageList.English);
                        Console.WriteLine("Языки изменены: Русско-английский");
                        break;
                    default:
                        Console.WriteLine(translator.Translate(text));
                        break;
                }
            }
        }
    }
}
