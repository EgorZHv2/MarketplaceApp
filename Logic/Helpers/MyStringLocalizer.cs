using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class MyStringLocalizer: IStringLocalizer
    {
         Dictionary<string, Dictionary<string, string>> resources;
        // ключи ресурсов
        const string HEADER = "Header";
        const string MESSAGE = "Message";
 
        public MyStringLocalizer()
        {
            // словарь для английского языка
            Dictionary<string, string> enDict = new Dictionary<string, string>
            {
                {HEADER, "Welcome" },
                {MESSAGE, "Hello World!" }
            };
            // словарь для русского языка
            Dictionary<string, string> ruDict = new Dictionary<string, string>
            {
                {HEADER, "Добо пожаловать" },
                {MESSAGE, "Привет мир!" }
            };
            // словарь для немецкого языка
           
            // создаем словарь ресурсов
            resources = new Dictionary<string, Dictionary<string, string>>
            {
                {"en", enDict },
                {"ru", ruDict },
            };
        }
        // по ключу выбираем для текущей культуры нужный ресурс
        public LocalizedString this[string name]
        {
            get
            {
                var currentCulture = CultureInfo.CurrentUICulture;
                string val = "";
                if (resources.ContainsKey(currentCulture.Name))
                {
                    if (resources[currentCulture.Name].ContainsKey(name))
                    {
                        val = resources[currentCulture.Name][name];
                    }
                }
                return new LocalizedString(name, val);
            }
        }
 
        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();
 
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
 
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return this;
        }
    }
}
