using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float propulsionForce = 10f;
    public float cooldown = 0.4f;
    private Rigidbody2D rb;
    private Animator smokeAnimator; // Animator para el humo
    private float lastPropulsionTime = 0f;
    private bool isPropelling = false;
    private float propulsionStartTime;

    [Header("Animacion")]
    

    private Animator animator;
    private string triggerIzquierda = "izquierda";
    private string triggerDerecha = "derecha";


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Obtener la referencia al Animator del humo, asumiendo que es un hijo llamado "SmokeEffect"
        //smokeAnimator = transform.Find("smoke").GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.RightArrow)) { 
            moveDirection.x = 1;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            animator.SetTrigger(triggerDerecha); 
            Invoke("ResetAnimation", 1f);
            }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
            moveDirection.x = -1; 
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            animator.SetTrigger(triggerIzquierda); 
            Invoke("ResetAnimation", 1f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { moveDirection.y = 1; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { moveDirection.y = -1; }


        if (moveDirection != Vector2.zero && Time.time - lastPropulsionTime >= cooldown)
        {
            ApplyPropulsion(moveDirection);
            // animator.SetTrigger("dash"); // Activar animación de propulsión del personaje
            //smokeAnimator.SetTrigger("dash"); // Activar animación de humo
        }

        if (isPropelling)
        {
            float timeSincePropulsion = Time.time - propulsionStartTime;
            float decelerationFactor = CalculateDecelerationFactor(timeSincePropulsion);
            rb.velocity *= decelerationFactor;

            // Verificar si es momento de detener la animación de propulsión
            if (decelerationFactor <= 0.05f) // Ajusta este valor según sea necesario
            {
                isPropelling = false;
            }
        }


    }

    void ApplyPropulsion(Vector2 direction)
    {
        rb.velocity = direction.normalized * propulsionForce;
        lastPropulsionTime = Time.time;
        propulsionStartTime = Time.time;
        isPropelling = true;
    }

    float CalculateDecelerationFactor(float timeSincePropulsion)
    {
        float decelerationDuration = 7;
        float decelerationCurve = timeSincePropulsion / decelerationDuration;
        float decelerationFactor = 1f - Mathf.Pow(decelerationCurve, 2);

        return Mathf.Clamp(decelerationFactor, 0.05f, 1f);
    }

    void ResetAnimation()
    {
        // Vuelve al estado normal de la animación
        animator.SetTrigger("idle"); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
