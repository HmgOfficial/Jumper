using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public enum Language
{
    es_es,
    en_us
}

[System.Serializable]
public class LanguageDictionaries
{
    public string key;
    public string es_ES;
    public string en_US;
    //public string ja_JP;
}

/// <summary>
/// Internationalization
/// </summary>
public class I18N : MonoBehaviour
{
    public static I18N Instance;

    public List<LanguageDictionaries> languageDictionaries;

    private int idLanguage = 0;
    private Language currentLanguage = Language.en_us;

    public delegate void OnLanguageChange();
    public event OnLanguageChange onLanguageChange;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Laguage") != idLanguage)
        {
            idLanguage = PlayerPrefs.GetInt("Language");
            ChangeLanguage(idLanguage);
        }
    }

    /// <summary>
    /// Returns the translated text
    /// </summary>
    /// <param name="key">The identifier</param>
    /// <returns></returns>
    public string TextToTranslate(string key)
    {
        foreach (LanguageDictionaries ld in languageDictionaries)
        {
            if (ld.key == key)
            {
                switch (currentLanguage)
                {
                    case Language.es_es:
                        return ld.es_ES;
                    case Language.en_us:
                        return ld.en_US;
                    default:
                        return ld.en_US;
                }
            }
        }
        Debug.LogError("Key not found");
        return null;
        //switch (currentLanguage)
        //{
        //    case Language.en_us:
        //        return en_us[key];
        //    case Language.es_es:
        //        return es_es[key];
        //    default:
        //        Debug.LogWarning("Language not supported");
        //        return en_us[key];
        //}
    }

    /// <summary>
    /// Cambia al idioma deseado
    /// </summary>
    /// <param name="index">Identificador del idioma || -1 para siguiente idioma</param>
    public void ChangeLanguage(int index)
    {
        if (index == -1)
        {
            idLanguage++;
            if (idLanguage > 1)
            {
                idLanguage = 0;
                PlayerPrefs.SetInt("Language", idLanguage);
            }
            switch (idLanguage)
            {
                //English
                case 0:
                    currentLanguage = Language.en_us;
                    break;
                //Spanish
                case 1:
                    currentLanguage = Language.es_es;
                    break;
                default:
                    currentLanguage = Language.en_us;
                    Debug.LogWarning("Language not suported");
                    break;
            }
        }
        else
        {
            switch (index)
            {
                //English
                case 0:
                    currentLanguage = Language.en_us;
                    break;
                //Spanish
                case 1:
                    currentLanguage = Language.es_es;
                    break;
                default:
                    currentLanguage = Language.en_us;
                    Debug.LogWarning("Language not suported");
                    break;
            }
            idLanguage = index;
            PlayerPrefs.SetInt("Language", idLanguage);
        }
        onLanguageChange();
    }
}
