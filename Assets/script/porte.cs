using UnityEngine;
using UnityEngine.SceneManagement; // Requis pour la gestion des scènes

public class porte : MonoBehaviour
{
    public string SceneName = "Untitled";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Cette fonction s'active automatiquement lorsqu'un objet traverse un Collider en mode "Is Trigger"
    private void OnTriggerEnter(Collider other)
    {
        // On vérifie que l'objet qui touche la porte est bien le joueur
        if (other.CompareTag("Player"))
        {
            // On charge la nouvelle scène
            SceneManager.LoadScene(SceneName);
        }
    }
}
