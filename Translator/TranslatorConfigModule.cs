using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class TranslatorConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITranslatorType>().To<YandexTranslator>();
            Bind<IFilterInputText>().To<FilterLowerCase>();
        }
    }
}
