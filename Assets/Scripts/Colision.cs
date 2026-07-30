using UnityEngine;
using UnityEngine.InputSystem;

public class Colision : MonoBehaviour
{
    [Header("Réglages")]
    public float dureeInvincibilite = 2.0f;
    public float delaiAvantRelevement = 0.5f;

    [Header("Matériaux (Apparence)")]
    public Material materialNormal;      // Le matériau classique du joueur
    public Material materialInvincible;  // Le matériau quand il est invincible (ex: clignotant, doré, transparent...)

    private Rigidbody rb;
    private Renderer renduJoueur;
    private DeplacementJoueur scriptDeplacement;

    private bool estAuSol = false;
    private bool estInvincible = false;

    private float chronoInvincible = 0f;
    private float chronoAttenteRelever = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scriptDeplacement = GetComponent<DeplacementJoueur>();

        // Récupère le Renderer (le composant qui affiche le matériau/couleur)
        renduJoueur = GetComponent<Renderer>();

        // Si le matériau normal n'est pas assigné dans l'Inspector, on prend celui par défaut
        if (materialNormal == null && renduJoueur != null)
        {
            materialNormal = renduJoueur.material;
        }
    }

    void Update()
    {
        // 1. Décompte et fin de l'invincibilité
        if (estInvincible)
        {
            chronoInvincible -= Time.deltaTime;
            if (chronoInvincible <= 0f)
            {
                DesactiverInvincibilite();
            }
        }

        // 2. Décompte du délai avant d'autoriser le redressement
        if (estAuSol)
        {
            if (chronoAttenteRelever > 0f)
            {
                chronoAttenteRelever -= Time.deltaTime;
            }
            else
            {
                if (Keyboard.current != null && (
                    Keyboard.current.zKey.isPressed || Keyboard.current.wKey.isPressed ||
                    Keyboard.current.qKey.isPressed || Keyboard.current.aKey.isPressed ||
                    Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed ||
                    Keyboard.current.spaceKey.isPressed))
                {
                    SeRelever();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (estInvincible || estAuSol) return;

        if (collision.gameObject.name.Contains("Boule") || collision.gameObject.CompareTag("Boule"))
        {
            CoucherAuSol();
        }
    }

    void CoucherAuSol()
    {
        estAuSol = true;
        chronoAttenteRelever = delaiAvantRelevement;

        if (scriptDeplacement != null) scriptDeplacement.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    void SeRelever()
    {
        estAuSol = false;

        transform.rotation = Quaternion.identity;
        transform.position += new Vector3(0, 0.5f, 0);

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        if (scriptDeplacement != null) scriptDeplacement.enabled = true;

        // Démarre l'invincibilité et change le Material
        ActiverInvincibilite();
    }

    void ActiverInvincibilite()
    {
        estInvincible = true;
        chronoInvincible = dureeInvincibilite;

        // Applique le matériau d'invincibilité
        if (renduJoueur != null && materialInvincible != null)
        {
            renduJoueur.material = materialInvincible;
        }
    }

    void DesactiverInvincibilite()
    {
        estInvincible = false;

        // Remet le matériau normal
        if (renduJoueur != null && materialNormal != null)
        {
            renduJoueur.material = materialNormal;
        }
    }
}