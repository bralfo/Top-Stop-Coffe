using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 target;
    public Animator animator;

    void Start()
    {
        target = transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movimento();

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
}


