using UnityEngine;
using UnityEngine.InputSystem;

public class Administrador_Touch : MonoBehaviour
{
    private Controles_Touch Controles_Touch;
    private void Awake(){
        Controles_Touch = new Controles_Touch();
    }
    private void OnEnable(){
        Controles_Touch.Enable();
    }
    private void OnDisable(){
        Controles_Touch.Disable();
    }
    void Start()
    {
        // Controles_Touch.Touch.TouchPress.started += ctx => StartTouch(ctx);
        // Controles_Touch.Touch.TouchPress.canceled += ctx => StartTouch(ctx);
    }
    private void StartTouch(InputAction.CallbackContext context){
        // Debug.Log("Touch started", Controles_Touch.TouchPosition.ReadValue<Vector2>() );
    }
    private void EndTouch(InputAction.CallbackContext context){
        Debug.Log("Touch ended");
    }
    void Update()
    {
        
    }
}
