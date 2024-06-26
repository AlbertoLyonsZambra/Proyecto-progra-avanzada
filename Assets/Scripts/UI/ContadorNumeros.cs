using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorNumeros : MonoBehaviour
{
    private TextMeshProUGUI tmpro;
    void Start()
    {
        tmpro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.parent.tag == "numeroV"){tmpro.text = "+" + PlayerPrefs.GetInt("MatsTallerV").ToString();}
        if(gameObject.transform.parent.tag == "numeroN"){tmpro.text = "+" + PlayerPrefs.GetInt("MatsTallerN").ToString();}
        if(gameObject.transform.parent.tag == "numeroR"){tmpro.text = "+" + PlayerPrefs.GetInt("MatsTallerR").ToString();}
        
    }
    
}
