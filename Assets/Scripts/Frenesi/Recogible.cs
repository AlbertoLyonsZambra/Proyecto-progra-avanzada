using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Recogible : MonoBehaviour
{
    public float attractionSpeed = 5f;

    void Update()
    {
        AttractConsumibleWithTag("ConsumibleV");
        AttractConsumibleWithTag("ConsumibleN");
        AttractConsumibleWithTag("ConsumibleR");
    }

    void AttractConsumibleWithTag(string tag)
    {
        GameObject consumible = GameObject.FindWithTag(tag);
        if (consumible != null)
        {
            Vector2 direction = (transform.position - consumible.transform.position).normalized;
            consumible.transform.position = Vector2.MoveTowards(consumible.transform.position, transform.position, attractionSpeed * Time.deltaTime);
        }
    }
}
