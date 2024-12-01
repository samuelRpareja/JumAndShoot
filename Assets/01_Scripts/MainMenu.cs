using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Indica los índices de los niveles en la Build Settings
    private int[] levelIndices = { 2, 3 };

    public void CargarNivel()
    {
        // Selecciona un índice aleatorio dentro del array de niveles
        int randomIndex = Random.Range(0, levelIndices.Length);

        // Carga la escena correspondiente
        SceneManager.LoadScene(levelIndices[randomIndex]);
    }

    public void CargarTienda()
    {
        SceneManager.LoadScene("Tienda");
    }

    public void BotonSalir()
    {
        Debug.Log("Saliendo");
        Application.Quit();
    }

    public void BotonVolver()
    {
        SceneManager.LoadScene("MenuInicial");
    }


}
