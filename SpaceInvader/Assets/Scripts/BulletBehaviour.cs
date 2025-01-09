using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 10f;
    [SerializeField]
    public float bulletTime = 5f;
    [SerializeField]
    public float specialBulletTime = 1f;

    void Start()
    {
        Destroy(this.gameObject, bulletTime);
    }

    // Update is called once per frame
    void Update()
    {
        bulletTime += Time.deltaTime;
        transform.Translate(0f, bulletSpeed * Time.deltaTime, 0f);
        Destroy(this.gameObject, bulletTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            bulletSpeed = 0f;
            transform.Translate(2f * Time.deltaTime, 0f, 0f);
            bulletTime = 1f;
        }
    }
}
