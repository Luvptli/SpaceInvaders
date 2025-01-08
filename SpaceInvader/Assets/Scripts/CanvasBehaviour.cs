using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject canvasMenu;
    [SerializeField]
    GameObject canvasOptions;
    [SerializeField]
    LeanTweenType animCurve;
    [SerializeField]
    GameObject canvasPause;
    [SerializeField]
    GameObject canvasGame;
    [SerializeField]
    GameObject canvasSelection;

    [SerializeField]
    PlayerBehaviour playerBehaviour;

    [SerializeField]
    float valueVolume;
    [SerializeField]
    Slider slideVolume;

    [SerializeField]
    Image imageMuteO;
    [SerializeField]
    Image imageMuteG;

    /*[SerializeField]
    Slider slideBrillo;
    [SerializeField]
    float valueBrillo;
    [SerializeField]
    Image imagenBrillo;*/

    [SerializeField]
    Toggle pantallaCompleta;

    [SerializeField]
    TMP_Dropdown resolutionDropdown;
    Resolution[] resoluciones;

    public bool estaJugando;
    void Start()
    {
        estaJugando = false;
        canvasMenu.SetActive(true);
        canvasOptions.SetActive(false);
        canvasGame.SetActive(false);
        canvasPause.SetActive(false);
        canvasSelection.SetActive(false);

        playerBehaviour.enabled = false;

        slideVolume.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slideVolume.value;
        Mute();

        /*slideBrillo.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        imagenBrillo.color = new Color(imagenBrillo.color.r, imagenBrillo.color.g, imagenBrillo.color.b, slideBrillo.value);*/

        if (Screen.fullScreen)
        {
            pantallaCompleta.isOn = true;
        }
        else
        {
            pantallaCompleta.isOn = false;
        }

        RevisarResolucion();
    }

    
    void Update()
    {
        slideVolume.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slideVolume.value;
        Mute();
        PauseGame();
    }

    public void StartButton()
    {
        canvasMenu.SetActive(false);
        canvasOptions.SetActive(false);
        estaJugando = false;
        canvasSelection.SetActive(true);
        playerBehaviour.enabled = true;
    }

    public void OptionsButton()
    {
        estaJugando = false;
        canvasGame.SetActive(false);
        LeanTween.moveLocalX(canvasMenu, -1920f, 1f).setOnComplete(() =>
        {
            canvasMenu.SetActive(false);
        });
        canvasOptions.SetActive(true);
        LeanTween.moveLocalX(canvasOptions, 0, 1f);
        canvasPause.SetActive(false);
    }

    public void VolumenSlide(float valor)
    {
        estaJugando = false;
        valueVolume = valor;
        PlayerPrefs.SetFloat("volumenAudio", valueVolume);
        AudioListener.volume = valueVolume;
        Mute();
    }

    public void Mute()
    {
        if (valueVolume == 0)
        {
            imageMuteO.enabled = true;
            imageMuteG.enabled = true;
        }
        else
        {
            imageMuteO.enabled = false;
            imageMuteG.enabled = false;
        }
    }

    /*public void BrilloSlide(float valor)
    {
        valueBrillo = valor;
        PlayerPrefs.SetFloat("brillo", valueBrillo);
        imagenBrillo.color = new Color(imagenBrillo.color.r, imagenBrillo.color.g, imagenBrillo.color.b, slideBrillo.value);
    }*/

        public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        resolutionDropdown.AddOptions(opciones);
        resolutionDropdown.value = resolucionActual;
        resolutionDropdown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void ReturnButton()
    {
        estaJugando = false;
        LeanTween.moveLocalX(canvasOptions, 1920, 1f).setOnComplete(() =>
        {
            canvasOptions.SetActive(false);
        });
        canvasMenu.SetActive(true);
        LeanTween.moveLocalX(canvasMenu, 7.9332f, 1f);
    }
    public void PauseGame()
    {
        if (estaJugando && Input.GetKeyDown(KeyCode.Escape))
        {
            canvasPause.SetActive(true);
            canvasGame.SetActive(false);
            estaJugando = false;
        }
        else if (estaJugando == false && canvasPause == true && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToGame();
        }
    }
    public void ReturnToGame()
    {
        canvasPause.SetActive(false);
        canvasGame.SetActive(true);
        estaJugando = true;
    }
   
    public void ExitButton()
    {
        estaJugando = false;
        Debug.Log("Salir");
        Application.Quit();
    }
}
