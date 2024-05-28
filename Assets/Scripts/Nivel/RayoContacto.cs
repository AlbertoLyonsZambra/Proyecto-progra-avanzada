using UnityEngine;

public class RayoContacto : MonoBehaviour
{
    private Color originalColor;
    private Renderer playerRenderer;
    private GameObject playerObject;
    public GameObject spriteParticlePrefab;
    private bool isColliding = false;
    private GameObject lastSprite;
    private Renderer asteroideRenderer; // Normal
    private Color asteroideColor;
    private Renderer matVerdeRenderer; // Verde
    private Color matVerdeColor;
    private Renderer matNaranRenderer; // Naranja
    private Color matNaranColor;
    private Renderer matRosaRenderer; // Rosa
    private Color matRosaColor;
    [SerializeField] private float distanciaDeteccion = 25f;

    void Awake()
    {
        playerObject = transform.parent.gameObject;
        playerRenderer = playerObject.GetComponent<Renderer>();
        if (playerRenderer != null){originalColor = playerRenderer.material.color;}
        if (!ColorUtility.TryParseHtmlString("#E89E9E", out asteroideColor)){Debug.LogError("Invalid color code");}
        if (!ColorUtility.TryParseHtmlString("#2DFD0B", out matVerdeColor)){Debug.LogError("Invalid color code");}
        if (!ColorUtility.TryParseHtmlString("#FF8300", out matNaranColor)){Debug.LogError("Invalid color code");}
        if (!ColorUtility.TryParseHtmlString("#FF08E5", out matRosaColor)){Debug.LogError("Invalid color code");}
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

        if (Physics.Raycast(rayo, out hitInfo, distanciaDeteccion))
        {
            if (hitInfo.collider.CompareTag("Obs_Asteroide"))
            {
                // Debug.Log("Rayo en contacto con asteroide");
                if (!isColliding)
                {
                    Vector3 offsetFromPlayer = direccionFija * -3f;
                    offsetFromPlayer.y = 1.5f;
                    lastSprite = Instantiate(spriteParticlePrefab, playerObject.transform.position + offsetFromPlayer, Quaternion.identity);
                    isColliding = true;
                }
                if (playerRenderer != null){playerRenderer.material.color = Color.red;}
                asteroideRenderer = hitInfo.collider.GetComponent<Renderer>();
                if (asteroideRenderer != null){asteroideRenderer.material.color = asteroideColor;}
            }
            else if(hitInfo.collider.CompareTag("MatNormal"))
            {
                // Debug.Log("Rayo en contacto con material verde");
                if (!isColliding)
                {
                    Vector3 offsetFromPlayer = direccionFija * -3f;
                    offsetFromPlayer.y = 1.5f;
                    lastSprite = Instantiate(spriteParticlePrefab, playerObject.transform.position + offsetFromPlayer, Quaternion.identity);
                    isColliding = true;
                }
                if (playerRenderer != null){playerRenderer.material.color = Color.red;}
                matVerdeRenderer = hitInfo.collider.GetComponent<Renderer>();
                if (matVerdeRenderer != null){matVerdeRenderer.material.color = asteroideColor;}
            }
            else if(hitInfo.collider.CompareTag("MatRaro"))
            {
                // Debug.Log("Rayo en contacto con material verde");
                if (!isColliding)
                {
                    Vector3 offsetFromPlayer = direccionFija * -3f;
                    offsetFromPlayer.y = 1.5f;
                    lastSprite = Instantiate(spriteParticlePrefab, playerObject.transform.position + offsetFromPlayer, Quaternion.identity);
                    isColliding = true;
                }
                if (playerRenderer != null){playerRenderer.material.color = Color.red;}
                matNaranRenderer = hitInfo.collider.GetComponent<Renderer>();
                if (matNaranRenderer != null){matNaranRenderer.material.color = asteroideColor;}
            }
            else if(hitInfo.collider.CompareTag("MatSuper"))
            {
                // Debug.Log("Rayo en contacto con material verde");
                if (!isColliding)
                {
                    Vector3 offsetFromPlayer = direccionFija * -3f;
                    offsetFromPlayer.y = 1.5f;
                    lastSprite = Instantiate(spriteParticlePrefab, playerObject.transform.position + offsetFromPlayer, Quaternion.identity);
                    isColliding = true;
                }
                if (playerRenderer != null){playerRenderer.material.color = Color.red;}
                matRosaRenderer = hitInfo.collider.GetComponent<Renderer>();
                if (matRosaRenderer != null){matRosaRenderer.material.color = asteroideColor;}
            }
        }
        else
        {
            //Debug.Log("Rayo no en contacto con asteroide");
            isColliding = false;
            if (lastSprite != null){Destroy(lastSprite); lastSprite = null;}
            if (playerRenderer != null){playerRenderer.material.color = originalColor;}
            if (asteroideRenderer != null){asteroideRenderer.material.color = Color.white;} // color original asteroide normal
            if (matVerdeRenderer != null){matVerdeRenderer.material.color = matVerdeColor;} // color original material verde
            if (matNaranRenderer != null){matNaranRenderer.material.color = matNaranColor;} // color original material naranja
            if (matRosaRenderer != null){matRosaRenderer.material.color = matRosaColor;} // color original material rosa
        }
        Debug.DrawRay(rayo.origin, rayo.direction * distanciaDeteccion, Color.red);
    }
}