using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CondicionVictoria", menuName = "ScriptableObjects/CondicionVictoria", order = 1)]
public class CondicionVictoria : ScriptableObject
{
    private GestorBarra gestorBarra;
    public bool victoria;

    // Start is called before the first frame update
    void Start()
    {
        gestorBarra = GestorBarra.Instance;
        victoria = gestorBarra.victoria;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
