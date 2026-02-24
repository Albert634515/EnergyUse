namespace WpfUI.Services;

public class LanguageService : ILanguageService
{
    public string Translate(string key, string fallback)
    {
        //return Languages.GetResourceString(key, fallback);
        return fallback;
    }
}