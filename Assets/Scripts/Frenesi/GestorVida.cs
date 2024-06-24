using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorVida : GenericSingleton<GestorVida>
{
    public int vida = 3;
    public GameObject[] corazones;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vida == 3)
        {
            corazones[0].SetActive(true);
            corazones[1].SetActive(true);
            corazones[2].SetActive(true);
        }
        else if (vida == 2) 
        {
            corazones[0].SetActive(true);
            corazones[1].SetActive(true);
            corazones[2].SetActive(false);
        }
        else if (vida == 1)
        {
            corazones[0].SetActive(true);
            corazones[1].SetActive(false);
            corazones[2].SetActive(false);
        }

        else if (vida == 0)
        {
            corazones[0].SetActive(false);
            corazones[1].SetActive(false);
            corazones[2].SetActive(false);
        }
    }


}
