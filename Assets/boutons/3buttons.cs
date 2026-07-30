using UnityEngine;
using UnityEngine.SceneManagement;

public class _3buttons : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject menuPrincipal; // Le Canvas ou le panneau du menu à désactiver

    public void StartGame()
    {
        // Enlève simplement le menu pour laisser place au jeu
        if (menuPrincipal != null)
        {
            menuPrincipal.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Veuillez assigner le menuPrincipal dans l'inspecteur !");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Le jeu se ferme...");
        Application.Quit();
    }
}