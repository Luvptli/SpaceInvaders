using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemiesBehaviour : MonoBehaviour
{
    public static AllEnemiesBehaviour instance;
    
    [SerializeField] private Vector3 initialPosition;

    [SerializeField] private float enemiesJumpAmount;

    [SerializeField] private Vector2 spaceBetweenEnemies;

    [SerializeField] private float columns;
    [SerializeField] private float rows;

    public float enemiesSpeed;

    [SerializeField] private GameObject[] enemiesPrefabs;
    [HideInInspector] public List<List<GameObject>> enemies = new List<List<GameObject>>();

    [SerializeField] private GameObject enemiesParent;

    [HideInInspector] public bool moving;
    private bool changeDirection;
    private bool colided;

    private int direction = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        CreateEnemies();
        moving = true;
    }

    private void Update()
    {
        EnemiesMovement();

        colided = false;
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < columns; i++)
        {
            enemies.Add(new List<GameObject>());
            for (int j = 0; j < rows; j++)
            {
                Vector3 position = new Vector3(initialPosition.x + i * spaceBetweenEnemies.x, initialPosition.y - j * spaceBetweenEnemies.y, initialPosition.z);
                int randomEnemy = Random.Range(0, enemiesPrefabs.Length);
                GameObject enemy = Instantiate(enemiesPrefabs[randomEnemy], position, enemiesPrefabs[randomEnemy].transform.rotation);
                enemy.name = $"Enemy ({i},{j})";
                enemy.gameObject.transform.SetParent(enemiesParent.transform);
                enemies[i].Add(enemy);
            }
        }
    }

    void EnemiesMovement()
    {
        if (moving)
        {
            foreach (var column in enemies)
            {
                foreach (var enemy in column)
                {
                    if (enemy.activeSelf)
                    {
                        enemy.transform.position += Vector3.right * direction * enemiesSpeed * Time.deltaTime;
                    }
                }
            }
        }
    }

    void MoveEnemiesDown()
    {
        foreach (var column in enemies)
        {
            foreach (var enemy in column)
            {
                if (enemy.activeSelf)
                {
                    enemy.transform.position += Vector3.down * enemiesJumpAmount;
                }
            }
        }
    }

    public void ChangeDirection()
    {
        if (!colided)
        {
            colided = true;
            direction *= -1;
            MoveEnemiesDown();
        }
    }

    public void CheckEnemiesAmount()
    {
        int count = 0;
        foreach (Transform child in enemiesParent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                count++;
            }
        }
        if (count == 0)
        {
            CanvasBehaviour.instance.ShowWinGame();
        }
    }
}
