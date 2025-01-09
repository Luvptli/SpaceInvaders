using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuerteBehaviour : MonoBehaviour
{
    [SerializeField]
    public float health = 4f;
    [SerializeField]
    public float bulletDamage = 1f;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet2" || other.tag == "Bullet")
        {
            if (health <= 0f)
            {
                Destroy(this.gameObject);
            }
            if (health >= 0f)
            {
                health -= bulletDamage;
            }
            Destroy(other.gameObject);
        }
    }
}
