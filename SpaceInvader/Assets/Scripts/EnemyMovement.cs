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
    float initialPosX = -7.8f; // Posici�n inicial en X
    [SerializeField]
    float initialPosY = 14.07f; // Posici�n inicial en Y
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
    float minXLimit = -10.0f; // L�mite m�nimo en X
    [SerializeField]
    float maxXLimit = 10.0f; // L�mite m�ximo en X
    [SerializeField]
    float fireChance = 0.1f; // Probabilidad de que los enemigos disparen

    [SerializeField]
    GameObject bulletPrefab; // Prefab de la bala (as� podr�s asignar el prefab de la bala en el Inspector)

    List<List<GameObject>> matrizObjetos = new List<List<GameObject>>(); // Lista de enemigos
    List<List<float>> disparoProbabilidad; // Probabilidad de disparo por columna
    float globalDirection = 1.0f; // Direcci�n global del movimiento

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

                // Calculamos la posici�n con los desplazamientos
                Vector3 position = new Vector3(initialPosX + xOffset, initialPosY, zOffset);
                position.x = position.x + i * spaceBetweenElementsX;
                position.y = position.y - j * spaceBetweenElementsY;

                // Seleccionar el prefab para esta fila
                GameObject prefab = prefabs[j % prefabs.Count];
                GameObject alien = Instantiate(prefab, position, Quaternion.identity);

                alien.name = "Alien(" + i.ToString() + "," + j.ToString() + ")";
                alien.transform.rotation = Quaternion.Euler(0, 90, 0); // Rotaci�n en Y de 90 grados

                matrizObjetos[i].Add(alien);

                // Inicializar la probabilidad de disparo para cada enemigo
                disparoProbabilidad[i].Add(j == totalRow - 1 ? fireChance : 0f); // Solo los enemigos de la �ltima fila tienen probabilidad de disparo
            }
        }
    }

    void Update()
    {
        // Mover todos los enemigos en la direcci�n global
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

        // Verificar si alg�n enemigo ha alcanzado los l�mites
        bool reachedLimit = false;
        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                if (matrizObjetos[i][j] != null)
                {
                    float positionX = matrizObjetos[i][j].transform.position.x;
                    // Verificar si se alcanz� un l�mite en el eje X
                    if (positionX <= minXLimit || positionX >= maxXLimit)
                    {
                        reachedLimit = true;
                        break;
                    }
                }
            }
            if (reachedLimit) break;
        }

        // Si se alcanz� el l�mite, cambiar la direcci�n global y mover todos los enemigos una fila hacia abajo
        if (reachedLimit)
        {
            globalDirection *= -1; // Cambiar direcci�n

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

        // Verificar si los enemigos de la �ltima fila disparan
        for (int i = 0; i < totalColumns; i++)
        {
            GameObject enemy = matrizObjetos[i][totalRow - 1]; // Obtenemos el enemigo de la �ltima fila en la columna actual
            if (enemy != null && Random.value < disparoProbabilidad[i][totalRow - 1]) // Verificamos si el enemigo dispara seg�n la probabilidad
            {
                Fire(enemy); // Disparar
            }
        }
    }

    // Funci�n para disparar
    void Fire(GameObject enemy)
    {
        // Instanciar la bala a partir del prefab
        GameObject bullet = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);

        // Ajustar la direcci�n de la bala, normalmente ir�a hacia abajo, dependiendo de tu dise�o
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0); // Aqu� es donde a�ades la velocidad
        Debug.Log(enemy.name + " dispara");
    }

    // Funci�n para destruir un enemigo
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
