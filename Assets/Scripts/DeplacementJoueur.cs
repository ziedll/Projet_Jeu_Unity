using UnityEngine;
using UnityEngine.InputSystem;

public class DeplacementJoueur : MonoBehaviour
{
    [Header("Réglages")]
    public float vitesse = 5.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void Update()
    {
        // Si le script est désactivé (quand le joueur est à terre), on arrête tout
        if (!enabled) return;

        if (Keyboard.current == null) return;

        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.qKey.isPressed) moveX = -1f;
        if (Keyboard.current.dKey.isPressed) moveX = 1f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.zKey.isPressed) moveZ = 1f;
        if (Keyboard.current.sKey.isPressed) moveZ = -1f;

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        if (rb != null)
        {
            Vector3 nouvelleVitesse = direction * vitesse;
            rb.linearVelocity = new Vector3(nouvelleVitesse.x, rb.linearVelocity.y, nouvelleVitesse.z);
        }
    }
}