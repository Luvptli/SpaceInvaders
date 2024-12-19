using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject canvasMenu;
    [SerializeField]
    GameObject canvasOptions;
    [SerializeField]
    LeanTweenType animCurve;

    bool estaJugando;
    void Start()
    {
        canvasMenu.SetActive(true);
        canvasOptions.SetActive(false);
        estaJugando = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        canvasMenu.SetActive(false);
        canvasOptions.SetActive(false);
        estaJugando =true;
    }

    public void OptionsButton()
    {
        estaJugando = false;
        LeanTween.moveLocalX(canvasMenu, -1920f, 1f).setOnComplete(() =>
        {
            canvasMenu.SetActive(false);
        });
        canvasOptions.SetActive(true);
        LeanTween.moveLocalX(canvasOptions, 0, 1f);
    }

    public void ExitButton()
    {
        estaJugando = false;
    }
}
