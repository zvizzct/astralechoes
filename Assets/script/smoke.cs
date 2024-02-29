using UnityEngine;

public class smoke : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerDashEffect()
    {
        animator.SetTrigger("dash");
    }
}
