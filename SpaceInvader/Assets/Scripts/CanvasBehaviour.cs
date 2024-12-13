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
        LeanTween.scale(canvasMenu, Vector3.one * 0.9f, 0.5f).setEase(animCurve).setOnComplete(() =>
        {
            LeanTween.scale(canvasMenu, Vector3.one * 1f, 0.5f);
        });
        estaJugando =true;
    }

    public void OptionsButton()
    {
        canvasOptions.SetActive(true);
        estaJugando = false;
    }

    public void ExitButton()
    {
        estaJugando = false;
    }
}
