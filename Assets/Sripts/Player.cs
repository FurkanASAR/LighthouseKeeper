using UnityEngine;

public class Player : MonoBehaviour
{    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;

    [SerializeField] private CharacterController characterController;

    private Vector3 velocity = new();
    private float gravity = -9.81f;

    private Inventory playerInventory;

    private void Awake()
    {
        if(playerInventory == null)
        {
            playerInventory = new Inventory();
        }
    }

    private void Update()
    {
        MovementHandler();
    }

    private void MovementHandler()
    {
        Vector2 inputVector = new();

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }

        if(inputVector != Vector2.zero && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 7f;
        }



        if (Input.GetKey(KeyCode.Space))
        {
            TryJump();
        }

        

        Vector3 movementDirection = transform.right * inputVector.x + transform.forward * inputVector.y;
        movementDirection.Normalize();

        velocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 finalMove = movementDirection + velocity;

        characterController.Move(finalMove * Time.deltaTime * speed);

    }

    private void TryJump()
    {
        if (characterController.isGrounded)
        {
            float jumpForce = this.jumpForce / 100;
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpForce);
        }
    }


    public Inventory GetInventory()
    {
        return playerInventory;
    }
}
