using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Recogible : MonoBehaviour
{
    
    private GestorBarra gestorBarra;
     // Booleano para controlar si se cuentan los consumibles

    private int consumibleV = 0;
    private int consumibleN = 0;
    private int consumibleR = 0;

    void Start()
    {
        gestorBarra = GestorBarra.Instance;
    }
    void Update()
    {
          guardarMats();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("ConsumibleV"))
        {
            consumibleV++;
            Debug.Log("ConsumibleV: " + consumibleV);
            Destroy(other.gameObject); // Destruir el consumible al ser recogido (opcional)
        }
        else if (other.CompareTag("ConsumibleN"))
        {
            consumibleN++;
            Debug.Log("ConsumibleN: " + consumibleN);
            Destroy(other.gameObject); // Destruir el consumible al ser recogido (opcional)
        }
        else if (other.CompareTag("ConsumibleR"))
        {
            consumibleR++;
            Debug.Log("ConsumibleR: " + consumibleR); 
            Destroy(other.gameObject); // Destruir el consumible al ser recogido (opcional)
        }
    }

    

    public void guardarMats()
    {
        PlayerPrefs.SetInt("MatsTallerV", PlayerPrefs.GetInt("MatsTallerV") + consumibleV);
        PlayerPrefs.SetInt("MatsTallerN", PlayerPrefs.GetInt("MatsTallerN") + consumibleN/2);
        PlayerPrefs.SetInt("MatsTallerR", PlayerPrefs.GetInt("MatsTallerR") + consumibleR/2);

        consumibleV = 0;
        consumibleN = 0;
        consumibleR = 0;

    }
}
