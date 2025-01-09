using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    int totalColumns = 12; // Total de columnas
    [SerializeField]
    int totalRow = 10; // Total de filas
    [SerializeField]
    float initialPosX = -7.8f; // Posición inicial en X
    [SerializeField]
    float initialPosY = 14.07f; // Posición inicial en Y
    [SerializeField]
    float spaceBetweenElementsX = 2.25f; // Espaciado entre columnas
    [SerializeField]
    float spaceBetweenElementsY = 1.5f; // Espaciado entre filas
    [SerializeField]
    List<GameObject> prefabs; // Lista de prefabs (4 prefabs, uno por fila)
    [SerializeField]
    List<float> rowXOffsets; // Lista de desplazamientos en X para cada fila
    [SerializeField]
    List<float> rowZOffsets; // Lista de desplazamientos en Z para cada fila
    [SerializeField]
    float movementSpeed = 2.0f; // Velocidad del movimiento
    [SerializeField]
    float minXLimit = -10.0f; // Límite mínimo en X
    [SerializeField]
    float maxXLimit = 10.0f; // Límite máximo en X
    [SerializeField]
    float fireChance = 0.1f; // Probabilidad de que los enemigos disparen

    [SerializeField]
    GameObject bulletPrefab; // Prefab de la bala (así podrás asignar el prefab de la bala en el Inspector)

    List<List<GameObject>> matrizObjetos = new List<List<GameObject>>(); // Lista de enemigos
    List<List<float>> disparoProbabilidad; // Probabilidad de disparo por columna
    float globalDirection = 1.0f; // Dirección global del movimiento

    void Start()
    {
        disparoProbabilidad = new List<List<float>>(); // Inicializamos la lista de probabilidades

        // Crear la matriz de enemigos y la probabilidad de disparo
        for (int i = 0; i < totalColumns; i++)
        {
            matrizObjetos.Add(new List<GameObject>());
            disparoProbabilidad.Add(new List<float>());

            for (int j = 0; j < totalRow; j++)
            {
                // Obtenemos los desplazamientos en X y Z de esta fila
                float xOffset = rowXOffsets[j];
                float zOffset = rowZOffsets[j];

                // Calculamos la posición con los desplazamientos
                Vector3 position = new Vector3(initialPosX + xOffset, initialPosY, zOffset);
                position.x = position.x + i * spaceBetweenElementsX;
                position.y = position.y - j * spaceBetweenElementsY;

                // Seleccionar el prefab para esta fila
                GameObject prefab = prefabs[j % prefabs.Count];
                GameObject alien = Instantiate(prefab, position, Quaternion.identity);

                alien.name = "Alien(" + i.ToString() + "," + j.ToString() + ")";
                alien.transform.rotation = Quaternion.Euler(0, 90, 0); // Rotación en Y de 90 grados

                matrizObjetos[i].Add(alien);

                // Inicializar la probabilidad de disparo para cada enemigo
                disparoProbabilidad[i].Add(j == totalRow - 1 ? fireChance : 0f); // Solo los enemigos de la última fila tienen probabilidad de disparo
            }
        }
    }

    void Update()
    {
        // Mover todos los enemigos en la dirección global
        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                if (matrizObjetos[i][j] != null)
                {
                    Vector3 position = matrizObjetos[i][j].transform.position;
                    position.x += globalDirection * movementSpeed * Time.deltaTime;
                    matrizObjetos[i][j].transform.position = position;
                }
            }
        }

        // Verificar si algún enemigo ha alcanzado los límites
        bool reachedLimit = false;
        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                if (matrizObjetos[i][j] != null)
                {
                    float positionX = matrizObjetos[i][j].transform.position.x;
                    // Verificar si se alcanzó un límite en el eje X
                    if (positionX <= minXLimit || positionX >= maxXLimit)
                    {
                        reachedLimit = true;
                        break;
                    }
                }
            }
            if (reachedLimit) break;
        }

        // Si se alcanzó el límite, cambiar la dirección global y mover todos los enemigos una fila hacia abajo
        if (reachedLimit)
        {
            globalDirection *= -1; // Cambiar dirección

            // Mover todos los enemigos una fila hacia abajo
            for (int i = 0; i < totalColumns; i++)
            {
                for (int j = 0; j < totalRow; j++)
                {
                    if (matrizObjetos[i][j] != null)
                    {
                        Vector3 position = matrizObjetos[i][j].transform.position;
                        position.y -= spaceBetweenElementsY; // Desplazar en Y
                        matrizObjetos[i][j].transform.position = position;
                    }
                }
            }
        }

        // Verificar si los enemigos de la última fila disparan
        for (int i = 0; i < totalColumns; i++)
        {
            GameObject enemy = matrizObjetos[i][totalRow - 1]; // Obtenemos el enemigo de la última fila en la columna actual
            if (enemy != null && Random.value < disparoProbabilidad[i][totalRow - 1]) // Verificamos si el enemigo dispara según la probabilidad
            {
                Fire(enemy); // Disparar
            }
        }
    }

    // Función para disparar
    void Fire(GameObject enemy)
    {
        // Instanciar la bala a partir del prefab
        GameObject bullet = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);

        // Ajustar la dirección de la bala, normalmente iría hacia abajo, dependiendo de tu diseño
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0); // Aquí es donde añades la velocidad
        Debug.Log(enemy.name + " dispara");
    }

    // Función para destruir un enemigo
    public void DestroyEnemy(int columnIndex, int rowIndex)
    {
        Destroy(matrizObjetos[columnIndex][rowIndex]);

        // Transferir la probabilidad de disparo al siguiente enemigo en la columna
        for (int j = totalRow - 1; j > 0; j--)
        {
            if (matrizObjetos[columnIndex][j] == null && matrizObjetos[columnIndex][j - 1] != null)
            {
                // El siguiente enemigo hereda la posibilidad de disparo
                disparoProbabilidad[columnIndex][j - 1] = Mathf.Min(disparoProbabilidad[columnIndex][j - 1] + 0.05f, 1.0f); // Incrementar la probabilidad hasta el 100%
                break;
            }
        }
    }
   
}
