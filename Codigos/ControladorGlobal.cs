using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGlobal : MonoBehaviour
{
    [SerializeField] public int idDelCheckpointActual;
    [SerializeField] public bool cambiarCheckpoint;
    [SerializeField] public bool NivelFinalizado;
    [SerializeField] public int muertesAcumuladas;
    [SerializeField] private int TotalColeccionables;
    [SerializeField] private int TotalRecogidos;

    // Inicia patrón singleton
    public static ControladorGlobal Instance;

    private void Awake()
    {
        if (ControladorGlobal.Instance == null)
        {
            ControladorGlobal.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        NivelFinalizado = false;
    }
    // Finaliza patrón singleton

    public void IncrementarMuertes()
    {
        muertesAcumuladas++;
    }

    private void Update() {
        NivelFinalizado = (TotalColeccionables - TotalRecogidos < 1);
    }

    public void ReiniciarValores()
    {
        idDelCheckpointActual = 0;
        cambiarCheckpoint = false;
        muertesAcumuladas = 0;
        TotalRecogidos = 0;
        NivelFinalizado = false;
        PausarJuego(false);
    }

    public void ReiniciarMundo()
    {
        ReiniciarValores();
    }

    // Actualiza el punto de guardado, retorna el ID del anterior punto
    public int ActivarPuntoDeGuardado(int nuevoId)
    {
        int paraRetornar = idDelCheckpointActual;        
        if (nuevoId != idDelCheckpointActual)
        {
            Debug.Log("Reemplazando " + idDelCheckpointActual + " por " + nuevoId);
            idDelCheckpointActual = nuevoId;
            cambiarCheckpoint = true;
        }
        return paraRetornar;
    }

    public void Salir()
    {
        // https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
        #if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ReiniciarEscena()
    {
        // Obtén el índice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        NivelFinalizado = false;

        // Carga la misma escena por su índice
        SceneManager.LoadScene(currentSceneIndex);
        PausarJuego(false);
    }

    public void PausarJuego(bool Estado)
    {
        Time.timeScale = Estado ? 0.0f : 1.0f; 
    }

    public void SetFinalizarEscena(bool Estado)
    {
        NivelFinalizado = Estado;
    }

    public bool GetFinalizarEscena()
    {
        return NivelFinalizado;
    }

    public void SetTotalColeccionables(int total)
    {
        TotalColeccionables = total;
    }

    public int GetTotalColeccionables()
    {
        return TotalColeccionables;
    }

    public void SetTotalRecogidos(int total = -99)
    {
        TotalRecogidos = total == -99 ? TotalRecogidos + 1 : total;
    }

    public int GetTotalRecogidos()
    {
        return TotalRecogidos;
    }

    public void CambiarNivel(int IdEscena)
    {
        ReiniciarValores();
        SceneManager.LoadScene(IdEscena);
    }
}