using UnityEngine;

public class RayoContacto : MonoBehaviour
{
    private Color originalColor;
    private Renderer playerRenderer;
    private GameObject playerObject;
    public GameObject spriteParticlePrefab;
    private bool isColliding = false;
    private GameObject lastSprite;

    void Awake()
    {
        playerObject = transform.parent.gameObject;
        playerRenderer = playerObject.GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }
    }

    void Update()
    {
        rayoContacto();
    }

    private void rayoContacto()
    {
        Vector3 direccionFija = Vector3.forward;
        Ray rayo = new Ray(transform.position, direccionFija);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, 25f))
        {
            if (hitInfo.collider.CompareTag("Obs_Asteroide"))
            {
                Debug.Log("Rayo en contacto con asteroide");

                if (!isColliding)
                {
                    Vector3 offsetFromPlayer = direccionFija * -3f;
                    offsetFromPlayer.y = 1.5f;
                    lastSprite = Instantiate(spriteParticlePrefab, playerObject.transform.position + offsetFromPlayer, Quaternion.identity);
                    isColliding = true;
                }

                if (playerRenderer != null)
                {
                    playerRenderer.material.color = Color.red;
                }
            }
            else
            {
                //Debug.Log("Rayo no en contacto con asteroide");

                isColliding = false;
                if (lastSprite != null)
                {
                    Destroy(lastSprite);
                    lastSprite = null;
                }

                if (playerRenderer != null)
                {
                    playerRenderer.material.color = originalColor;
                }
            }
        }
        else
        {
            //Debug.Log("Rayo no en contacto con asteroide");

            isColliding = false;
            if (lastSprite != null)
            {
                Destroy(lastSprite);
                lastSprite = null;
            }

            if (playerRenderer != null)
            {
                playerRenderer.material.color = originalColor;
            }
        }

        Debug.DrawRay(rayo.origin, rayo.direction * 25f, Color.red);
    }
}