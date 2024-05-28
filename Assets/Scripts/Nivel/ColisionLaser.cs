using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionLaser : MonoBehaviour
{
    [SerializeField] private GameObject MatRecogible;
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            other.transform.parent.gameObject.SetActive(false); //"Destruye" laser
            // gameObject.SetActive(false); //Desactiva la visibilidad del obstaculo
        }
        if (other.gameObject.CompareTag("Laser") && (tag == "MatNormal" || tag == "MatRaro" || tag == "MatSuper"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(MatRecogible, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
