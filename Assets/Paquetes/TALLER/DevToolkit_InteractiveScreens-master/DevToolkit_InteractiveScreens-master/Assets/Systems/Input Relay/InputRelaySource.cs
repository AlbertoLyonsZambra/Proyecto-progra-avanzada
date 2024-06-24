using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputRelaySource : MonoBehaviour
{
    [SerializeField] LayerMask RaycastMask = ~0;
    [SerializeField] float RaycastDistance = 15f;
    [SerializeField] UnityEvent<Vector2> OnCursorInput = new UnityEvent<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuPrincipal.Instance.enTerminal) {lol();}
    }
    private void lol()
    {
        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.x == float.NegativeInfinity || mousePosition.y == float.NegativeInfinity || 
            mousePosition.x < 0 || mousePosition.y < 0 || 
            mousePosition.x > Screen.width || mousePosition.y > Screen.height)
        {
            return;
        }

        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hitResult;
        if (Physics.Raycast(mouseRay, out hitResult, RaycastDistance, RaycastMask, QueryTriggerInteraction.Ignore))
        {
            if (hitResult.collider.gameObject != gameObject)
            {

                return;
            }

            OnCursorInput.Invoke(hitResult.textureCoord);
        }
    }
}
