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

        if(PlayerPrefs.GetInt("jugandoFrenesi", 0) == 0)
        {
            PlayerPrefs.SetInt("MatsTallerV", PlayerPrefs.GetInt("MatsTallerV") + PlayerPrefs.GetInt("MatsFrenesiV"));
            PlayerPrefs.SetInt("MatsTallerN", PlayerPrefs.GetInt("MatsTallerN") + PlayerPrefs.GetInt("MatsFrenesiN"));
            PlayerPrefs.SetInt("MatsTallerR", PlayerPrefs.GetInt("MatsTallerR") + PlayerPrefs.GetInt("MatsFrenesiR"));

            PlayerPrefs.SetInt("MatsFrenesiV", 0);
            PlayerPrefs.SetInt("MatsFrenesiN", 0);
            PlayerPrefs.SetInt("MatsFrenesiR", 0);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (PlayerPrefs.GetInt("jugandoFrenesi",0) == 1)
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
    }



        public void guardarMats()
        {
            PlayerPrefs.SetInt("MatsFrenesiV", PlayerPrefs.GetInt("MatsFrenesiV") + consumibleV);
            PlayerPrefs.SetInt("MatsFrenesiN", PlayerPrefs.GetInt("MatsFrenesiN") + consumibleN / 2);
            PlayerPrefs.SetInt("MatsFrenesiR", PlayerPrefs.GetInt("MatsFrenesiR") + consumibleR / 2);

            consumibleV = 0;
            consumibleN = 0;
            consumibleR = 0;

        }
    
}
