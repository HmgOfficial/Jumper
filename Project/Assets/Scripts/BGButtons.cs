using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the hue of the BG buttons
/// </summary>
public class BGButtons : MonoBehaviour
{

    public Image bgImage;
    int hue = 0;
    void Start()
    {
        if (bgImage == null)
        {
            GetComponent<Image>();
        }
        StartCoroutine("ChangeColor");
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            bgImage.color = Color.HSVToRGB((float)hue / 360, (float)53 / 100, 100 / 100);
            hue++;
            if (hue > 360)
            {
                hue = 0;
            }
            yield return 0;
        }
        
    }
}
