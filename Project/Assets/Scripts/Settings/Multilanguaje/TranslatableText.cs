using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslatableText : MonoBehaviour
{
    public string key;
    private TextMeshProUGUI txt;


    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        TranslateText();
        I18N.Instance.onLanguageChange += TranslateText;
    }

    private void TranslateText()
    {
        txt.text = I18N.Instance.TextToTranslate(key);
    }
}
