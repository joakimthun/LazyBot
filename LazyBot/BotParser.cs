using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyBot
{
    public class BotParser
    {
        public RequestKeys GetKeys(TextSelection selection)
        {
            //Example key
            //@ApiKey:f568d28a-8280-459d-a21d-80fd82b6cab2,Bot:24@

            selection.StartOfDocument(false);
            selection.SelectLine();
            var selectedText = selection.Text;
            var firstIndex = selectedText.IndexOf('@');
            var lastIndex = selectedText.IndexOf('@', firstIndex + 1);
            var keysPart = selectedText.Substring(firstIndex + 1, lastIndex - (firstIndex + 1));

            var keysArray = keysPart.Split(',');

            var keysList = new List<string>();
            for (int i = 0; i < keysArray.Length; i++)
            {
                var kvp = keysArray[i].Split(':');
                keysList.Add(kvp[1]);
            }

            return new RequestKeys { Key = keysList[0], Bot = keysList[1] };
        }

        public string GetDocument(TextSelection selection)
        {
            selection.SelectAll();
            return selection.Text;
        }
    }
}
