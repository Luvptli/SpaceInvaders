using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 10f;
    [SerializeField]
    public float bulletTime = 5f;
    void Start()
    {
        Destroy(this.gameObject, bulletTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (0f, bulletSpeed * Time.deltaTime, 0f);
    }
}
