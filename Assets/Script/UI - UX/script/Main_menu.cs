using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{
    public void PlayGame1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void PlayGame2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void PlayGame3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void PlayGame4()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void BackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
    }
    public void PlayGameActive()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    { 
        PlayerPrefs.SetInt("DeathCount", 1); // Remet le compteur à 1
        PlayerPrefs.Save(); // Sauvegarde la nouvelle valeur
        Application.Quit();

    }
    public void Update()
    {
        // Vérifie si la touche "Espace" est pressée
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("q a été pressé !");
            // Place ton action ici
        }
    }
}

