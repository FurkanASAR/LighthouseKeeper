using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Player : MonoBehaviour, IInteractor
{
    public InteractorType InteractorType => InteractorType.Player;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float SprintSpeed = 7f;
    [SerializeField] private float jumpForce = 1f;

    [SerializeField] private CharacterController characterController;

    private Vector3 velocity = new();
    private float gravity = -9.81f;

    private Inventory playerInventory;

    private void Awake()
    {
        if (playerInventory == null)
        {
            playerInventory = new Inventory();
        }
    }


    private void Start()
    {
        InputManager.Instance.OnJumpButtonPressed += TryJump;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        InputManager.Instance.OnJumpButtonPressed -= TryJump;
    }


    private void Update()
    {
        MovementHandler();
    }

    private void MovementHandler()
    {
        Vector2 inputVector = InputManager.Instance.GetMovementInput();

        Vector3 movementDirection = transform.right * inputVector.x + transform.forward * inputVector.y;
        movementDirection.Normalize();


        velocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 finalMove = movementDirection + velocity;


        float speed = InputManager.Instance.SprintKeyIsPressed() ? SprintSpeed : walkSpeed;

        characterController.Move(finalMove * Time.deltaTime * speed);
    }

    private void TryJump(object sender, EventArgs e)
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

    public bool HandleFixingObject(List<NeededItem> neededItems)
    {
        if (playerInventory.HasAllItems(neededItems))
        {
            playerInventory.RemoveNeededItems(neededItems);
            //Generator.Instance.FixObject();
            return true;
        }
        else
        {
            Debug.Log("You don't have all the required items to fix this object.");
            return false;
        }
    }
}
