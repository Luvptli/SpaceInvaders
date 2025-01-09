using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    float movementX;
    [SerializeField]
    public float playerSpeed = 7f;

    [SerializeField]
    public float minX = -70f;
    [SerializeField]
    public float maxX = -10f;

    [SerializeField]
    Vector3 posInicial;

    [SerializeField]
    public GameObject bullet;
    [SerializeField]
    public GameObject bulletSpecial;

    [SerializeField]
    GameObject canvasGameOver;

    [SerializeField]
    public float shootTime;
    [SerializeField]
    public float shootTimeSpecial;

    [SerializeField]
    public Vector3 offset = new Vector3(0, 1, 0);

    public CanvasBehaviour canvasBehaviour;

    void Start()
    {
        posInicial = transform.position;
        shootTime = 1f;
        shootTimeSpecial = 5f;
    }

    void Update()
    {
        shootTime += Time.deltaTime;
        shootTimeSpecial += Time.deltaTime;
        movementX = Input.GetAxisRaw("Horizontal");
        float newPosX = transform.position.x + movementX * playerSpeed * Time.deltaTime;
        newPosX = Mathf.Clamp (newPosX, minX, maxX);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Bullet();
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
        {
            BulletSpecial();
        }
    }

    void Bullet()
    {
        if (shootTime >= 1f)
        {
            Vector3 bulletPosition = transform.position + transform.TransformDirection(offset);
            Instantiate(bullet, bulletPosition, Quaternion.identity);
            shootTime = 0f;
        }
    }
    void BulletSpecial()
    {
        if (shootTimeSpecial >= 5)
        {
            Vector3 bulletPosition = transform.position + transform.TransformDirection(offset);
            Instantiate(bulletSpecial, bulletPosition, Quaternion.identity);
            shootTimeSpecial = 0f;
        }
    }
        

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet2")
        {
            Destroy(this.gameObject);
            CanvasBehaviour.Instance.ShowGameOver();
            CanvasBehaviour.Instance.StopGame();

        }
    }
}
