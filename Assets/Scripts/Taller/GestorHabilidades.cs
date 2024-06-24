using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestorHabilidades : GenericSingleton<GestorHabilidades>
{
    [Header("De la terminal")]
    [SerializeField] TextMeshProUGUI nombre;
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] TextMeshProUGUI precio;
    private int[] montoAComprar = new int[] { -1, -1, -1 };

    [Header("De la escena")]
    [SerializeField] Button[] AguanteChoque;
    private int cuantasVecesPuedeChocar;
    void Start()
    {
        cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar", 1); // empieza de 1, lo que es poco intuitivo, pero hace como que es 0, porque inicialmente puede chocar 0 veces para seguir vivo, pero si pongo cero no funciona bn y un webeo
        InicializarInteractuabilidad(AguanteChoque, cuantasVecesPuedeChocar);
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

    // limpia todo el panel negro de la izq de la terminal
    public void DesaparecerPanelDetalle(bool desaparecer)
    {
        Color disabledColor;
        ColorUtility.TryParseHtmlString("#000000", out disabledColor);
        if(desaparecer){disabledColor.a = 0f;}
        Image imagenDetalle = nombre.gameObject.transform.parent.gameObject.GetComponent<Image>();
        imagenDetalle.color = disabledColor;
        nombre.text = "";
        desc.text = "";
        precio.text = "";
    }

    public void SeleccionarHabilidad(string RAMAyLVL)
    {
        DesaparecerPanelDetalle(false);
        string rama;
        string nivel;
        if (!string.IsNullOrEmpty(RAMAyLVL) && RAMAyLVL.Length > 1)
        {
            rama = RAMAyLVL.Substring(0, RAMAyLVL.Length - 1);
            nivel = RAMAyLVL.Substring(RAMAyLVL.Length - 1); 
            if(rama == "AguanteChoque")
            {
                nombre.text = "Coraza   de   Titanio";
                desc.text = "Reviste   tu   nave   con   densas   placas   de   titanio   y   aguanta   un   impacto   extra."; 
                if(nivel == "1")
                { 
                    precio.text = "50 V";
                    montoAComprar[0] = 50;
                }
                else if(nivel == "2")
                { 
                    precio.text = "70 V,   15 N";  
                    montoAComprar[0] = 70;
                    montoAComprar[1] = 15;
                }
                else if(nivel == "3")
                { 
                    precio.text = "100 V,   45 N,   20 R";  
                    montoAComprar[0] = 100;
                    montoAComprar[1] = 45;
                    montoAComprar[2] = 20;
                }
            }   
        }     
        else{ print("el error esta en los botones de habilidades de la terminal"); }
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
