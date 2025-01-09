using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    public float health = 0f;
    [SerializeField]
    public float bulletDamage = 1f;
    [SerializeField]
    public float specialBulletDestructionTime = 1f;

    private void Start()
    {
        specialBulletDestructionTime = 0f;
    }
    private void Update()
    {
        specialBulletDestructionTime += Time.deltaTime;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Bullet")
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
        if (other.tag == "Bullet3")
        {
            if(health <= 0f || health >= 0f)
            {
                if (specialBulletDestructionTime >= 0.1f)
                {
                    Destroy(this.gameObject);
                    specialBulletDestructionTime = 0f;
                }
            }
        }
    }
}
