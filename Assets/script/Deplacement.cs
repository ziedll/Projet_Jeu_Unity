using TMPro;
using UnityEngine;

public class deplacement : MonoBehaviour
{
    [Header ("Déplacements")]
    public float vitesse = 6f;
    public float vitesseRotation = 10f;
    public Animator animator;

    // Variables pour le suivi de la caméra
    [Header("Paramètres de la Caméra")]
    [SerializeField] private Transform _cameraTransform;
    public Vector3 cameraOffset = new Vector3(0.18f, 3.5f, -8f);
    public float cameraSmoothSpeed = 5f;

    // Ajout d'une référence au composant Character Controller
    private CharacterController controller;

    [Header("Saut et Gravité")]
    public float hauteurSaut = 3.5f;
    public float gravite = -9.81f;
    private Vector3 velocite;

    void Start()
    {
        // On récupère le composant au lancement du jeu
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) z = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) z = -1f;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;

        Vector3 mouseP = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0f);
        Debug.Log(mouseP);
        _cameraTransform.rotation = Quaternion.Euler(mouseP);
        Vector3 dir = new Vector3(x, 0f, z).normalized;

        if (dir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * vitesseRotation);

        
            controller.Move(dir * vitesse * Time.deltaTime);

            if (animator != null) animator.SetBool("IsRunning", true);
        }
        else
        {
            if (animator != null) animator.SetBool("IsRunning", false);
        }

        // Vérifie si on touche le sol et qu'on ne monte plus
        if (controller.isGrounded && velocite.y < 0)
        {
            velocite.y = -2f; // Petite force pour maintenir le personnage collé au sol
        }

        // Action de sauter
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocite.y = Mathf.Sqrt(hauteurSaut * -2f * gravite);
            if(animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }

        // Ajout constant de la gravité 
        velocite.y += gravite * Time.deltaTime;

        // Application de ce mouvement vertical
        controller.Move(velocite * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Fait suivre la caméra de manière fluide après que le personnage ait bougé
        if (_cameraTransform != null)
        {
            Vector3 desiredPosition = transform.position + cameraOffset;
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, desiredPosition, cameraSmoothSpeed * Time.deltaTime);
            _cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
        
        }
    }
}