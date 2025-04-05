using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public static bool GameIsPause=false;
    public GameObject pausedMenuUI;
    public GameObject pauseSettingsUI;
   
    public void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if (GameIsPause)
            {
                Resume();
                
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pausedMenuUI.SetActive(false);
        pauseSettingsUI.SetActive(false);

        Time.timeScale = 1f;
        GameIsPause = false;
        musicSource.Play();


    }

    void Pause()
    {
        pausedMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        musicSource.Pause();
    }
    public void GoBack()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);

    }
}
