using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    GameObject canvasSelect;

    public static CanvasBehaviour Instance;
    [SerializeField]
    GameObject canvasGameOver;
    [SerializeField]
    GameObject canvasWin;

    [SerializeField]
    PlayerBehaviour playerBehaviour;
    [SerializeField]
    EnemyMovement enemysManager;

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

    private void Awake()
    {
        // Asegurarse de que solo exista una instancia de GameOverController
        if (Instance == null)
        {
            Instance = this; // Asignar la instancia
        }
        else
        {
            Destroy(gameObject); // Si ya existe, destruir este objeto
        }
    }
    void Start()
    {
        estaJugando = false;
        canvasMenu.SetActive(true);
        canvasOptions.SetActive(false);
        canvasSelect.SetActive(false);
        canvasPause.SetActive(false);
        canvasSelection.SetActive(false);
        canvasWin.SetActive(false);
        canvasGameOver.SetActive(false);
        enemysManager.enabled = false;
       // enemysManager.ResetEnemies();

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
        canvasGameOver.SetActive(false);
        canvasSelection.SetActive(true);
        canvasSelect.SetActive(true);
        estaJugando = false;
        canvasSelection.SetActive(true);
        playerBehaviour.enabled = true;
        enemysManager.enabled=true;
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
        canvasSelection.SetActive(false);
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

    public void ShowGameOver()
    {
        // Activa el canvas de Game Over
        canvasGameOver.SetActive(true);
    }
    public void PauseGame()
    {
        if (estaJugando && Input.GetKeyDown(KeyCode.Escape))
        {
            canvasPause.SetActive(true);
            canvasGame.SetActive(false);
            canvasSelection.SetActive(false);
            estaJugando = false;
            Time.timeScale = 0f;
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
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        estaJugando = false;
        Debug.Log("Salir");
        Application.Quit();
    }
}
