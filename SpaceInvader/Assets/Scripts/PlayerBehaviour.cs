using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
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
    public float shootTime;

    [SerializeField]
    public Vector3 offset = new Vector3(0, 1, 0);

    void Start()
    {
        posInicial = transform.position;
        shootTime = 1f;
    }

    void Update()
    {
        shootTime += Time.deltaTime;
        movementX = Input.GetAxisRaw("Horizontal");
        float newPosX = transform.position.x + movementX * playerSpeed * Time.deltaTime;
        newPosX = Mathf.Clamp (newPosX, minX, maxX);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Bullet();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletEnemy")
        {
            Destroy(this.gameObject);
        }
    }
}
