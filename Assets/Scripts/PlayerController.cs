using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float lateralSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Movimento contínuo pra frente
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;

        // Movimento lateral com teclas A/D ou setas ← →
        float moveX = Input.GetAxis("Horizontal"); // -1 (esquerda), 1 (direita)
        Vector3 lateralMove = transform.right * moveX * lateralSpeed * Time.fixedDeltaTime;

        // Combina movimento
        Vector3 finalMove = forwardMove + lateralMove;

        // Aplica movimento com física
        rb.MovePosition(rb.position + finalMove);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGrounded)

        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

