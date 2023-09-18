using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class Interfaces : MonoBehaviour
{ 
    [SerializeField] public bool pasarNivel;
    [SerializeField] public int indiceNivel;

    // Update is called once per frame
    void Update()
    {
        if (pasarNivel)
        {
            CambiarNivel(indiceNivel);
        }
    }
    
    public void CambiarNivel(int indice)
    {
        ReiniciarValores();
        SceneManager.LoadScene(indice);
    }

    public void Salir()
    {
        ControladorGlobal.Instance.Salir();
    }

    public void ReiniciarValores()
    {
        ControladorGlobal.Instance.ReiniciarValores();
    }
}