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
    public Transform shotPos;
    public float fireForce = 500f;
    public float fireSpread = 0.1f;
    public float detectionRadius = 10f;
    public LayerMask enemyLayer;
    public Transform detectionCircleTransform; // Cambiado de GameObject a Transform
    private GestorVida gestorVida;

    private GameObject closestEnemy;

    void Start()
    {
        gestorVida = GestorVida.Instance;
    }

    void Update()
    {
        movePlayer();
        UpdateAnimator();
        DetectEnemies();
        RotateTowardsEnemy();
        TryShoot();
        UpdateDetectionCircleOrientation(); // Nueva línea añadida
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
        closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                GameObject enemy = hitCollider.gameObject;
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null)
        {
            detectionCircleTransform.gameObject.SetActive(true);
        }
        else
        {
            if (detectionCircleTransform.gameObject.activeSelf)
            {
                StartCoroutine(FadeOutDetectionCircle());
            }
        }
    }

    void TryShoot()
    {
        if (closestEnemy != null && Time.time > fireNext)
        {
            fireNext = Time.time + fireRate;

            Vector3 directionToEnemy = (closestEnemy.transform.position - shotPos.position).normalized;
            directionToEnemy.y = 0;

            Vector3 shotPosition = new Vector3(shotPos.position.x, shotPos.position.y, shotPos.position.z);

            Rigidbody bulletInstance = Instantiate(bulletPrefab, shotPosition, Quaternion.LookRotation(directionToEnemy));
            bulletInstance.velocity = directionToEnemy * fireForce;
            bulletInstance.velocity = new Vector3(bulletInstance.velocity.x, 0, bulletInstance.velocity.z);

            //Debug.Log($"Disparando hacia el enemigo: {closestEnemy.name}");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void RotateTowardsEnemy()
    {
        Quaternion targetRotation;

        if (closestEnemy != null)
        {
            Vector3 directionToEnemy = (closestEnemy.transform.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(directionToEnemy);
        }
        else if (mov != Vector2.zero)
        {
            Vector3 movement = new Vector3(mov.x, 0f, mov.y);
            targetRotation = Quaternion.LookRotation(movement);
        }
        else
        {
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
    }

    IEnumerator FadeOutDetectionCircle()
    {
        SpriteRenderer sr = detectionCircleTransform.GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        float fadeDuration = 0.7f;
        float fadeSpeed = 1 / fadeDuration;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * fadeSpeed;
            sr.color = Color.Lerp(originalColor, Color.clear, t);
            yield return null;
        }

        detectionCircleTransform.gameObject.SetActive(false);
        sr.color = originalColor;
    }

    // Nuevo método añadido
    private void UpdateDetectionCircleOrientation()
    {
        if (detectionCircleTransform != null)
        {
            Vector3 localPosition = detectionCircleTransform.localPosition;
            
            detectionCircleTransform.SetParent(null);
            detectionCircleTransform.rotation = Quaternion.Euler(90, 0, 0);
            detectionCircleTransform.SetParent(transform);
            
            detectionCircleTransform.localPosition = localPosition;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gestorVida.vida--;
            Debug.Log($"Se reduce la vida a: {gestorVida.vida}");
        }
    }
    
}
