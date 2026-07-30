using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{ 

    public static PanelManager instance;

    [Header("UI")]
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textScoreFinal;
    public TextMeshProUGUI textMeilleurScore;
    public GameObject GameOverPanel;
    public GameObject startPanel;

    private bool _gameover = false;
    private int _score = 0;
    

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(false);
        }
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        Time.timeScale = 0f; 
    }
    void Update()
    {

    }

    public void AjouterPoint()
    {
        _score += 1 ;

        if (textScore != null)
        {
            textScore.text = "Score : " + _score;
        }
    }

    //menu start
    public void GameStart()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (_gameover)
        {
            return;
        }

        _gameover = true;

        Time.timeScale = 0f;

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
        }

        // Ajout meilleur score
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
}