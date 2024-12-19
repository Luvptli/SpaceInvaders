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

   /* [SerializeField]
    Image imageMuteO;
    [SerializeField]
    Image imageMuteG;*/

    bool estaJugando;
    void Start()
    {
        canvasMenu.SetActive(true);
        canvasOptions.SetActive(false);
        estaJugando = false;
        canvasGame.SetActive(false);
        canvasPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (estaJugando && Input.GetKeyDown(KeyCode.Escape))
        {
            canvasPause.SetActive(true);
            canvasGame.SetActive(false);
        }
       // Mute();
    }

    public void StartButton()
    {
        Debug.Log("xd");
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
    }

    /*public void Mute()
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
    }*/

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
}
