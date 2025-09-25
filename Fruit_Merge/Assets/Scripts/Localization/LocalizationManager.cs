using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _turkishButton;
    [SerializeField] private Button _spanishButton;
    [SerializeField] private Button _japaneseButton;
    [SerializeField] private Button _koreanButton;
    [SerializeField] private Button _chineseButton;
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _germanButton;
    [SerializeField] private Button _frenchButton;
    [SerializeField] private Button _italianButton;
    [SerializeField] private Button _portugueseButton;
    [SerializeField] private Button _arabicButton;
    [SerializeField] private Button _hindicButton;


    private void Awake()
    {
        _englishButton.onClick.AddListener(() => SetLanguage(E_Language.English));
        _turkishButton.onClick.AddListener(() => SetLanguage(E_Language.Turkish));
        _spanishButton.onClick.AddListener(() => SetLanguage(E_Language.Spanish));
        _japaneseButton.onClick.AddListener(() => SetLanguage(E_Language.Japanese));
        _koreanButton.onClick.AddListener(() => SetLanguage(E_Language.Korean));
        _chineseButton.onClick.AddListener(() => SetLanguage(E_Language.Chinese));
        _russianButton.onClick.AddListener(() => SetLanguage(E_Language.Russian));
        _germanButton.onClick.AddListener(() => SetLanguage(E_Language.German));
        _frenchButton.onClick.AddListener(() => SetLanguage(E_Language.French));
        _italianButton.onClick.AddListener(() => SetLanguage(E_Language.Italian));
        _portugueseButton.onClick.AddListener(() => SetLanguage(E_Language.Portuguese));
        _arabicButton.onClick.AddListener(() => SetLanguage(E_Language.Arabic));
        _hindicButton.onClick.AddListener(() => SetLanguage(E_Language.Hindi));
    }

    public void SetLanguage(E_Language language)
    {
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.LocaleName.Equals(language.ToString()))
            {
                LocalizationSettings.SelectedLocale = locale;
                return;
            }
        }
    }
}
