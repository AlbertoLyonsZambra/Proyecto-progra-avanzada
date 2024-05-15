using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionLaser : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            other.transform.parent.gameObject.SetActive(false); //"Destruye" obstaculo
            // gameObject.SetActive(false); //"Destruye" laser
        }
    }
}
