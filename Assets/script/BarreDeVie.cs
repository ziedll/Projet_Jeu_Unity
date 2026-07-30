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
        if (other.name == "Cube")
        {
            PrendreDegats();
        }
    }

    void PrendreDegats()
    {
        if (vieActuelle > 0)
        {
            vieActuelle--;
            coeurs[vieActuelle].SetActive(false);
        }
    }
}