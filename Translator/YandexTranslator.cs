using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;

namespace Translator
{
    public class YandexTranslator : ITranslatorType
    {
        string translationDirection = "";
        Dictionary<LanguageList,string> dictionaryLanguage = new Dictionary<LanguageList, string>()
        {
            { LanguageList.English, "en"},
            { LanguageList.Russian, "ru"}
        };
        static private string EncryptedToken = "adc8e0a92c78bd5ebc3c204815d8b63314a.62Z6fc.f02474d          e99c      ntsr.1.l.1129012409T08857aeed4";
        static private string KeyWordOne = "2 4 1 3 8 9 6 7 10 5";
        static private string KeyWordTwo = "8 9 4 1 5 2 3 10 7 6";

        public string Translate(string text)
        {
            if (text.Length > 0)
            {
                try
                {
                    WebRequest request = WebRequest.Create(String.Format("https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&text={1}&lang={2}",
                        Decoder.Decrypt(EncryptedToken,
                            KeyWordOne.Split().Select(x => Convert.ToInt32(x)).ToArray(),
                            KeyWordTwo.Split().Select(x => Convert.ToInt32(x)).ToArray()),
                        text,
                        translationDirection));

                    WebResponse response = request.GetResponse();

                    StringBuilder result = new StringBuilder();
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        string line;
                        if ((line = stream.ReadLine()) != null)
                        {
                            Translation translation = JsonConvert.DeserializeObject<Translation>(line);
                            foreach (string str in translation.Text)
                                result.Append(str);
                        }
                    }
                    return result.ToString();
                }
                catch (WebException ex)
                {
                    return ex.Message;
                }
            }
            else
                return "";
        }
        private string GetLanguagesDirection(LanguageList input, LanguageList output)
        {
            string inputLanguage = "";
            string outputLanguage = "";
            dictionaryLanguage.TryGetValue(input, out inputLanguage);
            dictionaryLanguage.TryGetValue(output, out outputLanguage);
            if (String.IsNullOrEmpty(inputLanguage) || String.IsNullOrEmpty(outputLanguage))
                throw new System.NotImplementedException("Language not registered");
            else
                return String.Format("{0}-{1}", inputLanguage, outputLanguage);
        }
        public void ChangeLanguages(LanguageList input, LanguageList output)
        {
            translationDirection = GetLanguagesDirection(input, output);
        }
    }
}
