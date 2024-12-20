using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    Image imageMuteO;
    [SerializeField]
    Image imageMuteG;

    [SerializeField]
    float valueVolume;
    [SerializeField]
    Slider slideVolume;

    public bool estaJugando;
    void Start()
    {
        canvasMenu.SetActive(true);
        canvasOptions.SetActive(false);
        estaJugando = false;
        canvasGame.SetActive(false);
        canvasPause.SetActive(false);
    }

    
    void Update()
    {
        slideVolume.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slideVolume.value;
        Mute();
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

    public void StartButton()
    {
        canvasMenu.SetActive(false);
        canvasOptions.SetActive(false);
        estaJugando = true;
        canvasGame.SetActive(true);
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

    public void ExitButton()
    {
        estaJugando = false;
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
    public void ReturnToGame()
    {
        canvasPause.SetActive(false);
        canvasGame.SetActive(true);
        estaJugando = true;
    }
    //como distorisonar la pantalla, nombre, no me sale volume
}
