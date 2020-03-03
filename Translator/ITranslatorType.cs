using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    public interface ITranslatorType
    {
        string Translate(string text);
        void ChangeLanguages(LanguageList input, LanguageList output);
    }
}
