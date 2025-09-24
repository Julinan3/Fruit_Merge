using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class FontManager : MonoBehaviour
{
    [Header("Font Assets")]
    [SerializeField] private TMP_FontAsset latinExtendedFont;
    [SerializeField] private TMP_FontAsset turkishFont;
    [SerializeField] private TMP_FontAsset arabicFont;
    [SerializeField] private TMP_FontAsset cjkFont;
    [SerializeField] private TMP_FontAsset indicFont;
    [SerializeField] private TMP_FontAsset cyrillicFont;

    private List<TextMeshProUGUI> allTexts = new List<TextMeshProUGUI>();

    void Awake()
    {
        // sahnedeki tüm textleri bul (dinamik olarak da eklenebilir)
        allTexts.AddRange(FindObjectsOfType<TextMeshProUGUI>(true));

        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        ApplyFont(LocalizationSettings.SelectedLocale);
    }

    void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    private void OnLocaleChanged(Locale locale)
    {
        ApplyFont(locale);
    }

    private TMP_FontAsset GetFontForLocale(string code)
    {
        switch (code)
        {
            case "ar":
                return arabicFont;
            case "ru":
                return cyrillicFont;
            case "zh":
            case "ja-JP":
            case "ko-KR":
                return cjkFont;
            case "hi":
                return indicFont;
            default: // en, fr, de, it, pt, es, tr-TR
                return latinExtendedFont;
        }
    }
    private void ApplyFont(Locale locale)
    {
        string code = locale.Identifier.Code;
        TMP_FontAsset chosenFont = GetFontForLocale(code);


        foreach (var txt in allTexts)
            txt.font = chosenFont;
    }
}

