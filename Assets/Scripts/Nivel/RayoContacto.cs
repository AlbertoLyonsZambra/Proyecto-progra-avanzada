using UnityEngine;

public class RayoContacto : MonoBehaviour
{
    private Color originalColor;
    private Renderer playerRenderer;
    private Renderer asteroideRenderer;
    private GameObject playerObject;
    public GameObject spriteParticlePrefab;
    private bool isColliding = false;
    private GameObject lastSprite;
    private Color asteroideColor;

    void Awake()
    {
        playerObject = transform.parent.gameObject;
        playerRenderer = playerObject.GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }
        if (!ColorUtility.TryParseHtmlString("#E89E9E", out asteroideColor))
        {
            Debug.LogError("Invalid color code");
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

                asteroideRenderer = hitInfo.collider.GetComponent<Renderer>();
                if (asteroideRenderer != null)
                {
                    asteroideRenderer.material.color = asteroideColor;
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
                if (asteroideRenderer != null)
                {
                    asteroideRenderer.material.color = Color.white; // Or the original color of the asteroide
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
            if (asteroideRenderer != null)
            {
                asteroideRenderer.material.color = Color.white; // Or the original color of the asteroide
            }
        }

        Debug.DrawRay(rayo.origin, rayo.direction * 25f, Color.red);
    }
}