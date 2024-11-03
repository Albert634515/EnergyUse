using System.Collections;
using System.Globalization;
using System.Resources;

namespace WinFormsEF.Managers;

internal class Languages
{
    /// <summary>
    /// Use resx resource reader to read the file in.
    /// https://msdn.microsoft.com/en-us/library/system.resources.resxresourcereader.aspx
    /// </summary>
    /// <param name="translationKey"></param>
    /// <returns>Translation</returns>
    public static string GetResourceString(string translationKey, string defaultMessage)
    {
        var resourceFileName = $@".\Languages\Lang_{CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}.resx";
        var translationValue = "";

        ResXResourceReader rsxr = new ResXResourceReader(resourceFileName);
        foreach (DictionaryEntry d in rsxr)
        {
            if (d.Key.ToString().ToLower() == translationKey.ToLower())
            {
                translationValue = d.Value.ToString().Trim();
                break;
            }
        }

        if (string.IsNullOrEmpty(translationValue))
            translationValue = defaultMessage;

        return translationValue;
    }
}