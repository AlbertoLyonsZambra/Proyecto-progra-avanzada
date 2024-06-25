using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enlaces : MonoBehaviour
{
    [SerializeField] public Button habilidadEntradaBTN;
    [SerializeField] public Button habilidadSalidaBTN;
    private Habilidad habilidadEntrada;
    private Habilidad habilidadSalida;
    private Image relleno;

    void Start()
    {
        habilidadEntrada = habilidadEntradaBTN.gameObject.GetComponent<Habilidad>();
        habilidadSalida = habilidadSalidaBTN.gameObject.GetComponent<Habilidad>();
        relleno = transform.Find("relleno").GetComponent<Image>();
    }

}
