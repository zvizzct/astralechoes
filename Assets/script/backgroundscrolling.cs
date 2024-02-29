using UnityEngine;

public class backgroundscroll : MonoBehaviour
{
    public float backgroundSpeed = 0.5f; // Ajusta esto según necesites
    private Material material;

    void Start()
    {
        // Obtiene el material del Renderer para modificar su textura.
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Mueve la textura del material verticalmente.
        Vector2 offset = new Vector2(0, Time.time * backgroundSpeed);
        material.mainTextureOffset = offset;
    }
}
