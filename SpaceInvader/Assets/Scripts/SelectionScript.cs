using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameSelection;

    [SerializeField]
    GameObject naveElegida;

    [SerializeField]
    GameObject canvasSelecion;

    [SerializeField]
    GameObject imageGame;

    [SerializeField]
    GameObject enemysManager;

    [SerializeField]
    GameObject canvasGame;

    private void Start()
    {
        imageGame.SetActive(false);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Bullet"))
        {
            gameSelection.SetActive(false);
            canvasSelecion.SetActive(false);
            imageGame.SetActive(true);
            enemysManager.SetActive(true);
            canvasGame.SetActive(true);
            Destroy(other.gameObject);
            Instantiate(naveElegida);
        }
    }
}
