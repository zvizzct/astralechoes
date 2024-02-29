using System.Collections;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public GameObject[] meteoritePrefabs; // Arreglo de prefabs de meteoritos
    public float spawnInterval = 2f; // Intervalo de tiempo entre cada generación de meteoritos
    public Transform playerTransform; // Referencia al Transform del jugador

    private void Start()
    {
        StartCoroutine(SpawnMeteorites());
    }

    private IEnumerator SpawnMeteorites()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnMeteorite();
        }
    }

    void SpawnMeteorite()
    {
        GameObject meteoritePrefab = meteoritePrefabs[Random.Range(0, meteoritePrefabs.Length)];
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect; // Corregido para el aspect ratio actual

        // Ajustando los valores para asegurar la generación fuera de la pantalla
        float spawnYMin = Camera.main.transform.position.y + cameraHeight / 2f + 1f; // Justo por encima de la pantalla
        float spawnXMin = Camera.main.transform.position.x - cameraWidth / 2f - 1f;
        float spawnXMax = Camera.main.transform.position.x + cameraWidth / 2f + 1f;

        // spawnYMax no es necesario a menos que quieras una variación en la altura de generación
        float spawnY = spawnYMin + 4; // O puedes usar Random.Range(spawnYMin, spawnYMax) si tienes un spawnYMax definido
        float spawnX = Random.Range(spawnXMin, spawnXMax);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject meteorite = Instantiate(meteoritePrefab, spawnPosition, Quaternion.identity);


        // Decide si el meteorito se dirigirá hacia el jugador basado en una probabilidad del 60%
        if (Random.value < 0.6f && playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - meteorite.transform.position).normalized;
            Meteorite meteoriteScript = meteorite.GetComponent<Meteorite>();
            if (meteoriteScript != null)
            {
                meteoriteScript.SetDirection(direction);
            }
        }
        else // Si no se dirige al jugador, elige una dirección aleatoria hacia abajo
        {
            float angle = Random.Range(-45f, 45f); // Ángulo aleatorio para variar la dirección de caída hacia abajo
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.down;
            Meteorite meteoriteScript = meteorite.GetComponent<Meteorite>();
            if (meteoriteScript != null)
            {
                meteoriteScript.SetDirection(direction);
            }
        }
    }
}
