using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestorHabilidades : GenericSingleton<GestorHabilidades>
{
    [Header("De la escena")]
    [SerializeField] Button[] AguanteChoque;
    private int cuantasVecesPuedeChocar;
    void Start()
    {
        cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar", 1); // empieza de 1, lo que es poco intuitivo, pero hace como que es 0, porque inicialmente puede chocar 0 veces para seguir vivo, pero si pongo cero no funciona bn y un webeo
        InicializarInteractuabilidad(AguanteChoque, cuantasVecesPuedeChocar);
    }

    
    void Update()
    {
        
    }

    // inicializa el arbol de habilidades con las que ya estan compradas y las que no
    private void InicializarInteractuabilidad(Button[] botonesHabilidades, int habilidadComprada)
    {
        for (int i = 0; i < botonesHabilidades.Length; i++)
        {
            Button boton = botonesHabilidades[i];
            if (i == habilidadComprada - 1){boton.interactable = true;}
            else{boton.interactable = false;}
        }
    }
    
    public void ComprarAguanteChoque()
    {   // no se que esta pasando pero tiene sentido, apruebo
        if (cuantasVecesPuedeChocar - 1 < AguanteChoque.Length)
        {
            Color disabledColor;
            ColorUtility.TryParseHtmlString("#007906", out disabledColor);

            var boton = AguanteChoque[cuantasVecesPuedeChocar - 1];
            var colors = boton.colors;

            PlayerPrefs.SetInt("cuantasVecesPuedeChocar", cuantasVecesPuedeChocar + 1);
            cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar");

            InicializarInteractuabilidad(AguanteChoque, cuantasVecesPuedeChocar);
            boton.interactable = false; 
            colors.disabledColor = disabledColor;
        }
        else{ print("Compraste toda la línea de aguantar choques."); }
    }

    public bool ChoqueAsteroide(int vecesQueHaChocado)
    {
        if (vecesQueHaChocado < cuantasVecesPuedeChocar){return false;}
        return true;
    }

}
