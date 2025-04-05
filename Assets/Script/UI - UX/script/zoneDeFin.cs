using UnityEngine;

public class ZoneDeFin : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    public static bool GameIsPaused = false;
    public GameObject finMenuUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Vérifie que c'est bien le joueur
        {
            Debug.Log("Le joueur a atteint la zone de fin.");
            Finish();
        }
    }

    void Finish()
    {
        finMenuUI.SetActive(true);
        Time.timeScale = 0f; // Met le jeu en pause
        GameIsPaused = true;

        if (musicSource != null) // Vérifie si la musique existe avant de la mettre en pause
        {
            musicSource.Pause();
        }
    }
}