using UnityEngine;
using UnityEngine.UI;

public class Ayuda : MonoBehaviour
{
    [SerializeField] private GameObject pantallaAyudaJuego;
    [SerializeField] private GameObject pantallaAyudaTaller;
    [SerializeField] private Button botonAyuda;

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

        // Asigna el método MostrarAyuda al evento OnClick del botón
        botonAyuda.onClick.AddListener(MostrarAyuda);
    }

    public void MostrarAyuda()
    {
        // Alternar el estado de activación del GameObject pantallaAyudaJuego
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
}
