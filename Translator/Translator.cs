using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    public class Translator
    {
        ITranslatorType TranslatorType { get; set; }
        IFilterInputText FilterInputText { get; set; }


        public Translator(ITranslatorType translatorType, IFilterInputText filterInputText)
        {
            TranslatorType = translatorType;
            FilterInputText = filterInputText;

            ChangeLanguages(LanguageList.English, LanguageList.Russian);
        }
        public Translator(ITranslatorType translatorType, IFilterInputText filterInputText, LanguageList input, LanguageList output)
        {
            TranslatorType = translatorType;
            FilterInputText = filterInputText;

            ChangeLanguages(input, output);
        }

        public string Translate(string text)
        {                                                                       
            return TranslatorType.Translate(FilterInputText.FilterInputText(text));
        }

        public void ChangeLanguages(LanguageList input, LanguageList output)
        {
            TranslatorType.ChangeLanguages(input, output);
        }
    }
}
