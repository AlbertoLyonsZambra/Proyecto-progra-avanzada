using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorHabilidades : GenericSingleton<GestorHabilidades>
{
    private int cuantasVecesPuedeChocar;
    void Start()
    {
        cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar", 2);
    }

    
    void Update()
    {
        
    }

    public bool ChoqueAsteroide(int vecesQueHaChocado)
    {
        if (vecesQueHaChocado < cuantasVecesPuedeChocar){return false;}
        return true;
    }
}
