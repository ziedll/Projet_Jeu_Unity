using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SpawnerBoules : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Config SpawnerBoules")]

    public GameObject prefabBoules;

    // le temps entre chaque Spawn les ballss
    public float SpawnTimeBoules = 1.0f;
    public float BoulesDestroy = 5.0f;

    [Header("Config Zone Spawn")]
    [Header("Gauche")]
    public float decalageMinX = -3.0f;
    [Header("Droite")]
    public float decalageMaxX = 3.0f;

    void Start()
    {
        InvokeRepeating(nameof(BoulesSpawn), 0f, SpawnTimeBoules);
    }

    void BoulesSpawn()
    {
        if (prefabBoules != null)
        {
            //calcul de la rdm Position 
            float posXRdm = Random.Range(decalageMinX,decalageMaxX);
            //Nouvelle position du Spawner
            Vector3 positionSpawn = transform.position + new Vector3(posXRdm, 0, 0);

            //aparation Boules
            GameObject NewBoules = Instantiate(prefabBoules, positionSpawn,transform.rotation);
            //DestructioN Boules 
            Destroy(NewBoules, BoulesDestroy);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
