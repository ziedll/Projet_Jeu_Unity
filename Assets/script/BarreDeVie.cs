using UnityEngine;
using System.Collections.Generic;

public class BarreDeVie : MonoBehaviour
{
    public List<GameObject> coeurs;
    private int vieActuelle;

    void Start()
    {
        vieActuelle = coeurs.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Boules")
        {
            PrendreDegats();
        }
    }

    // LE MOT "public" EST AJOUTÉ ICI POUR DÉBLOQUER L'ACCÈS
    public void PrendreDegats()
    {
        if (vieActuelle > 0)
        {
            vieActuelle--;
            coeurs[vieActuelle].SetActive(false);

            // --- VÉRIFICATION DU GAME OVER ---
            if (vieActuelle <= 0)
            {
                // Utilisation du GameManager au lieu du PanelManager
                if (GameManager.instance != null)
                {
                    GameManager.instance.GameOver();
                }
                else
                {
                    Debug.LogWarning("GameManager introuvable dans la scène !");
                }
            }
            // ------------------------------------------------
        }
    }
}