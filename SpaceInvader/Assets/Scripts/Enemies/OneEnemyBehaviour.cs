using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEnemyBehaviour : MonoBehaviour
{
    [SerializeField] float[] shootInterval;
    [SerializeField] float shootTimer;

    [SerializeField] GameObject bulletGameObject;

    bool checkBounds = true;

    float gameOverBound = 2f;

    [SerializeField] private Vector3 bulletSpawnPosition;

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= Random.Range(shootInterval[0], shootInterval[1]))
        {
            shootTimer = 0f;

            if (IsBottomMost())
            {
                if (Random.Range(1, 7) == 1)
                {
                    Shoot();
                }
            }
        }

        if (gameObject.transform.position.y <= gameOverBound && checkBounds)
        {
            checkBounds = false;
            CanvasBehaviour.instance.ShowGameOver();
        }
    }

    private bool IsBottomMost()
    {
        Vector3 position = transform.position;

        foreach (var column in AllEnemiesBehaviour.instance.enemies)
        {
            if (column.Contains(gameObject))
            {
                foreach (var enemy in column)
                {
                    if (enemy.activeSelf && enemy.transform.position.y < position.y)
                    {
                        return false;
                    }
                }
                break;
            }
        }
        return true;
    }

    private void Shoot()
    {
        if (AllEnemiesBehaviour.instance.moving)
        {
            Instantiate(bulletGameObject, transform.position + bulletSpawnPosition, Quaternion.identity);
        }
    }
}
