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
        UpdateAnimator();

        // Actualizar los parámetros del Animator

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mov = context.ReadValue<Vector2>();
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(mov.x, 0f, mov.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            transform.Translate(movement * velocidadMovimiento * Time.deltaTime, Space.World);
        }
    }

    private void UpdateAnimator()
    {
        bool isMoving = mov != Vector2.zero;
        animator.SetBool("isCorrer", isMoving);
    }
}
