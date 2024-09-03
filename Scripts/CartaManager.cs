using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showCard : MonoBehaviour
{
    public GameObject[] cartas;// Array de referencias a las cartas del deck (debe contener 50 cartas)
    public float escalaMaxima = 2f; // Escala máxima de la carta
    public float tiempoTransicion = 0.5f; // Tiempo de transición para mostrar la carta
    public GameObject cartaPrefab; // Prefab de la carta para instanciar en el centro

    private GameObject cartaActual; // Referencia a la carta actual que se muestra en el centro
    private Vector3 posicionCentro; // Posición en el centro de la pantalla
    private bool isMouseOver = false; // Controlar el estado del mouse

    void Start()
    {
        // Asegúrate de que el array de cartas tenga 50 elementos
        if (cartas.Length != 50)
        {
            Debug.LogError("El array de cartas debe contener exactamente 50 cartas.");
            return; // Salir si no hay 50 cartas
        }

        // Calcular la posición en el centro de la pantalla
        posicionCentro = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
        posicionCentro.z = 0; // Asegúrate de que esté en la capa visible
    }

    void Update()
    {
        // Si hay una carta actual y el mouse está sobre ella
        if (isMouseOver && cartaActual != null)
        {
            cartaActual.transform.position = Vector3.Lerp(
                cartaActual.transform.position,
                posicionCentro,
                tiempoTransicion * Time.deltaTime
            );
            cartaActual.transform.localScale = Vector3.Lerp(
                cartaActual.transform.localScale,
                Vector3.one * escalaMaxima,
                tiempoTransicion * Time.deltaTime
            );
        }
    }

    private void OnMouseEnter()
    {
        // Cuando el mouse entra en una carta, crear una copia en el centro
        if (cartaActual == null)
        {
            int cartaIndex = Random.Range(0, cartas.Length);
            cartaActual = Instantiate(cartaPrefab, posicionCentro, Quaternion.identity); // Crear la copia de la carta
            cartaActual.transform.localScale = Vector3.one; // Asegurarse de que la escala inicial sea normal
        }
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        // Cuando el mouse sale de una carta
        isMouseOver = false;
        if (cartaActual != null)
        {
            Destroy(cartaActual); // Destruir la carta en el centro cuando el mouse sale
            cartaActual = null; // Resetear la referencia
        }
    }

    public void MostrarCarta()
    {
        // Este método puede no ser necesario si solo muestras la carta al pasar el mouse
        // Puedes dejarlo vacío o implementarlo según tu lógica
    }
}
