using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorSpace : MonoBehaviour
{
    public float velocidadMovimiento;
    private Vector2 mov;
    public Animator animator;
    public float fireRate = 0.5f;
    private float fireNext = 0.0f;
    public Rigidbody bulletPrefab;
    public Transform[] shotPos;
    public float fireForce = 500f;
    public float fireSpread = 0.1f;
    public GameObject muzFlashPrefab;
    public float detectionRadius = 10f;
    public LayerMask enemyLayer; // Asegúrate de configurar esto en el Inspector

    void Update()
    {
        movePlayer();
        UpdateAnimator();
        DetectEnemies();
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

    void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        List<GameObject> detectedEnemies = new List<GameObject>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                GameObject enemy = hitCollider.gameObject;
                if (!detectedEnemies.Contains(enemy))
                {
                    detectedEnemies.Add(enemy);
                    Debug.Log($"Enemigo detectado: {enemy.name} en posición {enemy.transform.position}");
                }
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}