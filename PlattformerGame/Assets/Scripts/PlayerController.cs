using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player movement")]

    [Tooltip("Adjusts the speed of the player")]
    [SerializeField] private float movementSpeed = 10.0f;


    [Tooltip("Adjusts the force of the players jump")]
    [SerializeField] private float jumpForce = 1.0f;


    [Tooltip("Adjusts the gravityscale")]
    [SerializeField] private float gravityScale = 1.0f;

    private Vector3 moveDirection;

    private CharacterController controller;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        float currentY = moveDirection.y;
        moveDirection.y = currentY;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * movementSpeed;
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);
    }
}

