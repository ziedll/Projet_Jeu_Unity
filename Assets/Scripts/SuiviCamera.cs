using UnityEngine;

public class SuiviCamera : MonoBehaviour
{
    [Header("Cible à suivre")]
    public Transform cible; // Glisse ton Joueur ici dans l'Inspector

    [Header("Réglages")]
    public Vector3 decalage; // La distance fixe entre la caméra et le joueur

    void Start()
    {
        // Si le décalage n'est pas réglé dans l'Inspector, 
        // on calcule le décalage actuel par défaut
        if (cible != null && decalage == Vector3.zero)
        {
            decalage = transform.position - cible.position;
        }
    }

    void LateUpdate()
    {
        // LateUpdate est appelé après Update, c'est idéal pour la caméra
        if (cible != null)
        {
            // On applique UNIQUEMENT la position de la cible + le décalage
            // La caméra conserve sa propre rotation (celle définie dans Start)
            transform.position = cible.position + decalage;
        }
    }
}