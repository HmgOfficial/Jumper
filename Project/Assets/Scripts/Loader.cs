using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que controla la pantalla de Cargando
/// </summary>
public class Loader : MonoBehaviour
{
    public GameObject loadingPanel;
    public Image progressBar;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Load")
        {
            LoadDesiredScene("Menu");
        }
    }

    /// <summary>
    /// Se encarga de iniciar la corrutina de carga de escena
    /// </summary>
    /// <param name="desiredScene">La scenea deseada</param>
    public void LoadDesiredScene(string desiredScene)
    {
        progressBar.fillAmount = 0;
        StartCoroutine(LoadYourSceneAsync(desiredScene));
        if (!loadingPanel.activeInHierarchy)
        {
            loadingPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Se encarga de cargar la escena mostrando el panel de carga
    /// </summary>
    /// <param name="desiredScene"></param>
    /// <returns></returns>
    private IEnumerator LoadYourSceneAsync(string desiredScene)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(desiredScene, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            progressBar.fillAmount = asyncLoad.progress;
            yield return null;
        }
        //else if (asyncLoad)

        progressBar.fillAmount = 1f;

        asyncLoad.allowSceneActivation = true;
    }

    /// <summary>
    /// Cierra el programa
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }
}
