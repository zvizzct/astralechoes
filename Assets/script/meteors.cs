using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private float speed; // Velocidad a la que cae el meteorito
    private Vector2 movementDirection = Vector2.down; // Dirección de movimiento inicial
    private float rotationSpeed; // Velocidad de rotación
    private int rotationDirection; // Dirección de la rotación (1 para horario, -1 para antihorario)

    void Start()
    {
        // Establece una velocidad de caída aleatoria entre 5 y 8
        speed = Random.Range(5f, 8f);

        // Establece un tamaño aleatorio para el meteorito
        float scale = Random.Range(0.1f, 0.5f);
        transform.localScale = new Vector3(scale, scale, scale);

        // Decide aleatoriamente la dirección de rotación y establece una velocidad de rotación
        rotationDirection = Random.value < 0.5f ? 1 : -1; // 50% de probabilidad para cada dirección
        rotationSpeed = Random.Range(50f, 100f); // Velocidad de rotación aleatoria
    }

    public void SetDirection(Vector2 direction)
    {
        movementDirection = direction;
    }

    void Update()
    {
        // Mueve el meteorito en la dirección establecida a la velocidad definida
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        // Rota el meteorito
        transform.Rotate(0f, 0f, rotationSpeed * rotationDirection * Time.deltaTime);

        // Destruye el meteorito si sale de la pantalla
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }
}
