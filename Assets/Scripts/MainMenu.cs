using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Invoke("fadeOut", 1f);
    }

    void fadeOut()
    {
        CrossFade.fadeOut();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        CrossFade.fadeIn();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
