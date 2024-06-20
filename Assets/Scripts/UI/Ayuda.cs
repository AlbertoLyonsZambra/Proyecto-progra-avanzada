using UnityEngine;
using UnityEngine.UI;

public class Ayuda : MonoBehaviour
{
    [SerializeField] private GameObject pantallaAyudaJuego;
    [SerializeField] private GameObject pantallaAyudaTaller;
    [SerializeField] private Button botonAyuda;
    [SerializeField] private Button botonCerrarAyuda; // Nuevo botón para cerrar la pantalla de ayuda
    [SerializeField] private Button botonCerrarAyudaT;

    private enum PantallaActual
    {
        Juego,
        Tutorial
        // Añade más pantallas aquí si es necesario
    }

    private PantallaActual pantallaActual;

    private void Start()
    {
        // Asegúrate de que las pantallas de ayuda estén desactivadas al inicio
        pantallaAyudaJuego.SetActive(false);
        pantallaAyudaTaller.SetActive(false);

        // Asigna los métodos a los eventos OnClick de los botones
        botonAyuda.onClick.AddListener(MostrarAyuda);
        botonCerrarAyuda.onClick.AddListener(CerrarAyuda); // Asigna el método CerrarAyuda al botón de cerrar
        botonCerrarAyudaT.onClick.AddListener(CerrarAyuda); // Asigna el método CerrarAyuda al botón de cerrar
    }

    public void MostrarAyuda()
    {
        // Alternar el estado de activación del GameObject pantallaAyudaJuego o pantallaAyudaTaller
        bool nuevaVisibilidad = !ObtenerVisibilidadActual();

        // Desactiva todas las pantallas de ayuda primero
        pantallaAyudaJuego.SetActive(false);
        pantallaAyudaTaller.SetActive(false);

        // Activa la pantalla de ayuda correspondiente según la pantalla actual
        switch (pantallaActual)
        {
            case PantallaActual.Juego:
                pantallaAyudaJuego.SetActive(nuevaVisibilidad);
                break;
            case PantallaActual.Tutorial:
                pantallaAyudaTaller.SetActive(nuevaVisibilidad);
                break;
            // Añade más casos aquí si es necesario
        }
    }

    private bool ObtenerVisibilidadActual()
    {
        switch (pantallaActual)
        {
            case PantallaActual.Juego:
                return pantallaAyudaJuego.activeSelf;
            case PantallaActual.Tutorial:
                return pantallaAyudaTaller.activeSelf;
            default:
                return false;
        }
    }

    // Método para cambiar la pantalla actual desde otros scripts
    public void CambiarPantallaActual(string pantalla)
    {
        switch (pantalla)
        {
            case "juego":
                pantallaActual = PantallaActual.Juego;
                break;
            case "taller":
                pantallaActual = PantallaActual.Tutorial;
                break;
            // Añade más casos aquí si es necesario
        }
    }

    // Nuevo método para cerrar la pantalla de ayuda
    public void CerrarAyuda()
    {
        Debug.Log("CerrarAyuda llamado");
        pantallaAyudaJuego.SetActive(false);
        pantallaAyudaTaller.SetActive(false);
    }

}
