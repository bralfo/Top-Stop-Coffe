using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 target;
    private Rigidbody2D rb;

    public Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isMoving;

    void Start()
    {
        target = transform.position;

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleInput();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();

            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            target = new Vector3(
                worldPos.x,
                worldPos.y,
                transform.position.z
            );
        }
    }

    private void MovePlayer()
    {
        Vector2 newPosition = Vector2.MoveTowards(
            rb.position,
            target,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPosition);
    }

    private void UpdateAnimation()
    {
        isMoving = Vector2.Distance(rb.position, target) > 0.05f;

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