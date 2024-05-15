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
           Vector3 posicionInicial = new Vector3(601, -25, 2000);
           Campo.transform.position = posicionInicial;
        }
    }
}
