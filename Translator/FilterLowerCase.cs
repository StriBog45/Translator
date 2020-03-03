using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class FilterLowerCase : IFilterInputText
    {
        public string FilterInputText(string text)
        {
            return text.ToLower();
        }
    }
}
