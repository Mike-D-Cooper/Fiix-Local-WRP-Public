using Local_WRP.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

public class AzureTranslatorServiceFactory
{
    private readonly ApplicationDbContext _dbContext;
    private readonly HttpClient _httpClient;

    public AzureTranslatorServiceFactory(ApplicationDbContext dbContext, HttpClient httpClient)
    {
        _dbContext = dbContext;
        _httpClient = httpClient;
    }

    public async Task<AzureTranslatorService> CreateAsync()
    {
        var keyEntry = await _dbContext.TenantDetails
            .FirstOrDefaultAsync();

        if (keyEntry == null)
            throw new Exception("Translator API key not found in the database.");

        return new AzureTranslatorService(
            _httpClient,
            "https://api.cognitive.microsofttranslator.com",
            keyEntry.AzureTranslatorKey,
            keyEntry.AzureTranslatorRegion,
            _dbContext
        );
    }
}

public class AzureTranslatorService
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;
    private readonly string _subscriptionKey;
    private readonly string _region;
    private readonly ApplicationDbContext _db;

    public AzureTranslatorService(HttpClient httpClient, string endpoint, string subscriptionKey, string region, ApplicationDbContext db)
    {
        _httpClient = httpClient;
        _endpoint = endpoint;
        _subscriptionKey = subscriptionKey;
        _region = region;
        _db = db;
    }

    public async Task<AzureTranslatorService> CreateAsync()
    {
        var keyEntry = await _db.TenantDetails
            .FirstOrDefaultAsync();

        if (keyEntry == null)
            throw new Exception("Translator API key not found in the database.");

        return new AzureTranslatorService(
            _httpClient,
            "https://api.cognitive.microsofttranslator.com",
            keyEntry.AzureTranslatorKey,
            keyEntry.AzureTranslatorRegion,
            _db
        );
    }

    public async Task<string> TranslateAsync(string text, string toLanguage, string fromLanguage = null)
    {
        if (text != null && text.Length > 0)
        {
            var existingTranslation = _db.Translations
            .Where(x => x.ToLanguage == toLanguage && x.FromPhrase == text)
            .FirstOrDefault();
            if (existingTranslation != null)
            {
                return existingTranslation.ToPhrase;
            }
            else
            {
                string result = "";
                for (int i = 0; i < 5; i++)
                {
                    result = await TranslateAsyncFromAzure(text, toLanguage, fromLanguage);
                    if (result != null) break;
                }
                return result == null ? "" : result;
            }
        }
        else
        {
            return text;
        }
    }

    public async Task<string> TranslateAsyncFromAzure(string text, string toLanguage, string fromLanguage = null)
    {
        string translatedText;

        if (_subscriptionKey != null && _subscriptionKey.Trim() != "")
        {
            var route = $"/translate?api-version=3.0&to={toLanguage}";
            if (!string.IsNullOrEmpty(fromLanguage))
                route += $"&from={fromLanguage}";

            var requestBody = new[] { new { Text = text } };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", _region);

            var response = await _httpClient.PostAsync(_endpoint + route, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);
            translatedText = doc.RootElement[0].GetProperty("translations")[0].GetProperty("text").GetString();
            var detectedLanguage = doc.RootElement[0].GetProperty("detectedLanguage").GetProperty("language").GetString();

            _db.Translations.Add(new Translation
            {
                FromLanguage = detectedLanguage,
                ToLanguage = toLanguage,
                FromPhrase = text,
                ToPhrase = translatedText
            });
            _db.SaveChanges();
        }
        else
        {
            translatedText = text;
        }
        return translatedText;
    }

    public async Task<T> TranslateUIElements<T>(object obj, string Language)
    {
        obj = Activator.CreateInstance<T>();
        if (Language != "en")
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                var name = prop.Name;
                var value = prop.GetValue(obj);
                prop.SetValue(obj, await TranslateAsync(value.ToString(), Language));
            }
        }
        return (T)obj;
    }
}

public static class AzureTranslatorLanguages
{
    public static readonly Dictionary<string, string> SupportedLanguages = new()
    {
        { "af", "Afrikaans" },
        { "ar", "Arabic" },
        { "bg", "Bulgarian" },
        { "bn", "Bangla" },
        { "bs", "Bosnian" },
        { "ca", "Catalan" },
        { "cs", "Czech" },
        { "cy", "Welsh" },
        { "da", "Danish" },
        { "de", "German" },
        { "el", "Greek" },
        { "en", "English" },
        { "es", "Spanish" },
        { "et", "Estonian" },
        { "fa", "Persian" },
        { "fi", "Finnish" },
        { "fr", "French" },
        { "gu", "Gujarati" },
        { "he", "Hebrew" },
        { "hi", "Hindi" },
        { "hr", "Croatian" },
        { "hu", "Hungarian" },
        { "id", "Indonesian" },
        { "it", "Italian" },
        { "ja", "Japanese" },
        { "kk", "Kazakh" },
        { "km", "Khmer" },
        { "kn", "Kannada" },
        { "ko", "Korean" },
        { "lo", "Lao" },
        { "lt", "Lithuanian" },
        { "lv", "Latvian" },
        { "ml", "Malayalam" },
        { "mr", "Marathi" },
        { "ms", "Malay" },
        { "mt", "Maltese" },
        { "mww", "Hmong Daw" },
        { "nb", "Norwegian Bokmål" },
        { "nl", "Dutch" },
        { "or", "Odia" },
        { "pa", "Punjabi" },
        { "pl", "Polish" },
        { "pt", "Portuguese" },
        { "ro", "Romanian" },
        { "ru", "Russian" },
        { "sk", "Slovak" },
        { "sl", "Slovenian" },
        { "sm", "Samoan" },
        { "sr-Cyrl", "Serbian (Cyrillic)" },
        { "sr-Latn", "Serbian (Latin)" },
        { "sv", "Swedish" },
        { "sw", "Swahili" },
        { "ta", "Tamil" },
        { "te", "Telugu" },
        { "th", "Thai" },
        { "tlh", "Klingon" },
        { "tr", "Turkish" },
        { "uk", "Ukrainian" },
        { "ur", "Urdu" },
        { "uz", "Uzbek" },
        { "vi", "Vietnamese" },
        { "yua", "Yucatec Maya" },
        { "zh-Hans", "Chinese Simplified" },
        { "zh-Hant", "Chinese Traditional" }
    };
}