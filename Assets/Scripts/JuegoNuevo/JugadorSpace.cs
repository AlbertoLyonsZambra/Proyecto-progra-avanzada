using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorSpace : MonoBehaviour
{
    public float velocidadMovimiento;
    private Vector2 mov;
    public Animator animator;

    void Update()
    {
        movePlayer();

        // Actualizar los parámetros del Animator
       
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mov = context.ReadValue<Vector2>();
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(mov.x, 0f, mov.y);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 015f);

        transform.Translate(movement * velocidadMovimiento * Time.deltaTime, Space.World);
    }
}
