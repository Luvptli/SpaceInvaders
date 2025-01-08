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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("eh");
            gameSelection.SetActive(false);
            canvasSelecion.SetActive(false);
            Destroy(other.gameObject);
            Instantiate(naveElegida, Vector3.one, Quaternion.identity);
        }
    }
}
