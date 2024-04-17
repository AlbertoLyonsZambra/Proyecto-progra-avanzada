using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReubicacionCampo : MonoBehaviour
{
    public GameObject Campo;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Collider_DP"))
        {
            /*
             * public GameObject Generador
            Destruccion del campo generado
            Campo.SetActive(false);
            Destroy(Campo);
            Destroy(Generador);
            Destroy(gameObject);
             */
            Vector3 posicionInicial = new Vector3(601, -25, 2000);
                Campo.transform.position = posicionInicial;

        }
    }
}
