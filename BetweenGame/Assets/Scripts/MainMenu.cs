using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject titlePanel;
    public GameObject controlsPanel;

    public void PlayClicked()
    {
        Debug.Log("Play");
        titlePanel.SetActive(false);
        controlsPanel.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitClicked()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
