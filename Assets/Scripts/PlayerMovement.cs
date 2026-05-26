using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 target;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isMoving;

    void Start()
    {
        target = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movimento();
        UpdateAnimation();
    }

    private void Movimento()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            target.z = transform.position.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void UpdateAnimation()
    {

        isMoving = Vector3.Distance(transform.position, target) > 0.01f;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            Vector3 direction = (target - transform.position).normalized;
            if (spriteRenderer != null && Mathf.Abs(direction.x) > 0.1f)
            {
                spriteRenderer.flipX = direction.x < 0;
            }
        }


    }
}