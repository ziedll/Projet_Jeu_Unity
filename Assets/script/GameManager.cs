using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textScoreFinal;
    public TextMeshProUGUI textMeilleurScore;
    public GameObject GameOverPanel;
    public GameObject startPanel;

    // --- AJOUT : Panel de Victoire ---
    [Header("Win UI")]
    public GameObject GameWinPanel;

    private bool _gameover = false;
    private bool _gameWon = false; // --- AJOUT : État de victoire ---
    private int _score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AjouterPoint()
    {
        _score += 1;

        if (textScore != null)
        {
            textScore.text = "Score : " + _score;
        }
    }

    public void GameOver()
    {
        // On empêche le Game Over si on a déjà gagné ou perdu
        if (_gameover || _gameWon) return;

        _gameover = true;
        Time.timeScale = 0f;

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
        }

        SauvegarderEtAfficherScore();
    }

    public void GameWin()
    {
        // On empêche la victoire si on a déjà gagné ou perdu
        if (_gameover || _gameWon) return;

        _gameWon = true;
        Time.timeScale = 0f; // Met le jeu en pause

        if (GameWinPanel != null)
        {
            GameWinPanel.SetActive(true);
        }

        SauvegarderEtAfficherScore();
    }

    private void SauvegarderEtAfficherScore()
    {
        int meilleurScoreSauvegarde = PlayerPrefs.GetInt("MeilleurScore", 0);

        if (_score > meilleurScoreSauvegarde)
        {
            meilleurScoreSauvegarde = _score;
            PlayerPrefs.SetInt("MeilleurScore", meilleurScoreSauvegarde);
            PlayerPrefs.Save();
        }

        if (textScoreFinal != null)
        {
            textScoreFinal.text = "Score Final : " + _score;
        }

        if (textMeilleurScore != null)
        {
            textMeilleurScore.text = "Meilleur Score : " + meilleurScoreSauvegarde;
        }
    }

    // --- AJOUT : Fonction pour redémarrer le niveau ---
    public void RestartGame()
    {
        // 1. Remettre le temps à la normale (très important !)
        Time.timeScale = 1f;

        // 2. Recharger la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}