namespace WpfUI.Services;

public interface ILanguageService
{
    string Translate(string key, string fallback);
}