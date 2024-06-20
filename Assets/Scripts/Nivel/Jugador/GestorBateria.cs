using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GestorBateria : MonoBehaviour
{
    
    private JugadorNivel jugador;
    public List<GameObject> cubos;

    void Start()
    {
        jugador = JugadorNivel.Instance;
        comenzarCoroutine();
    }

    void Update()
    {
        jugador = JugadorNivel.Instance;
        if (jugador.bateria == 0)
        {
            SetVisibilidadCubo(0);
            
        }
        else if (jugador.bateria > 0 && jugador.bateria <= 20f)
        {
            CambiarColor(Color.grey);
            SetVisibilidadCubo(1);
            if(jugador.bateria == 19){ InstanciadorObjetos.Instance.GenerarUnObjeto(InstanciadorObjetos.Instance.bateria, "Bateria"); }
        }
        else if (jugador.bateria > 20f && jugador.bateria <= 40f)
        {
            CambiarColor(Color.red);
            SetVisibilidadCubo(2);
        }
        else if (jugador.bateria > 40f && jugador.bateria <= 60f)
        {
            CambiarColor(Color.magenta);
            SetVisibilidadCubo(3);
        }
        else if (jugador.bateria > 60f && jugador.bateria <= 80f)
        {
            CambiarColor(Color.yellow);
            SetVisibilidadCubo(4);
        }
        else if (jugador.bateria > 80f && jugador.bateria <= 100f)
        {
            SetVisibilidadCubo(5);
            CambiarColor(Color.green);
        }
    }

    IEnumerator decrementarBateria()
    {
        while (true) // La coroutine nunca se detendr�
        {
            if (jugador.bateria > 0)
            {
                jugador.bateria--;
                // Debug.Log("Valor de la bateria: " + jugador.bateria);
            }
            else
            {
                //Debug.Log("Bater�a agotada");
            }

            yield return new WaitForSeconds(0.8f);
        }
    }

    public void comenzarCoroutine()
    {
        StartCoroutine(decrementarBateria());
    }
    public void CambiarColor(Color nuevoColor)
    {
        foreach (GameObject cubo in cubos)
        {
            Renderer renderer = cubo.GetComponent<Renderer>();
            if (renderer != null){renderer.material.color = nuevoColor;}
        }
    }
    public void SetVisibilidadCubo(int numeroVisible)
    {
        for (int i = 0; i < cubos.Count; i++)
        {
            Renderer renderer = cubos[i].GetComponent<Renderer>();
            if (renderer != null){renderer.enabled = (i < numeroVisible);}
        }
    }

    


}
