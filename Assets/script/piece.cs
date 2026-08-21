using UnityEngine;

public class Piece : MonoBehaviour
{
    public float vitesseRotation = 150f;
    public AudioClip SonCoin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // La ligne Destroy(gameObject, 10); a été supprimée ici.
        // La pièce restera maintenant à l'écran tant que le joueur ne la touche pas.
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, vitesseRotation * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision avec : " + other.gameObject.name + " |Tag : " + other.tag);

        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.GameWin();
        }
        else
        {
            Debug.LogError("GameManager.instance est introuvable !");
        }

        // jouer du son
        if (SonCoin != null)
        {
            AudioSource.PlayClipAtPoint(SonCoin, transform.position);
        }

        
        Destroy(gameObject);
    }
}