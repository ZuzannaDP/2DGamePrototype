using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    public float walkSpeed = 1f;
    public float sprintSpeed = 10f;
    public float sprintCost = 2f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D moveFilter;

    Vector2 moveInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    BoxCollider2D coll;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    public PlayerStaminaManager playerStaminaManager;
    private Coroutine sprint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        moveSpeed = walkSpeed;
    }

    private void FixedUpdate() {
        if (canMove) {
            // If movement input is not 0, try to move
            if (moveInput != Vector2.zero) {
                bool success = TryMove(moveInput);

                // If not successful try to move in only x direction
                if (!success) {
                    success = TryMove(new Vector2(moveInput.x, 0));
                }

                // If not successful try to move in only y direction
                if (!success) {
                    success = TryMove(new Vector2(0, moveInput.y));
                }

                // Set parameters for animations
                animator.SetFloat("Horizontal", moveInput.x);
                animator.SetFloat("Vertical", moveInput.y);
                animator.SetBool("isMoving", true);
            } else {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        if (direction != Vector2.zero) {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                moveFilter, // The settings that determine where a collision can occur on, such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    void OnMove(InputValue moveVal) {
        moveInput = moveVal.Get<Vector2>();
    }

    void OnInteract() {
        Debug.Log("Attempting to interact");

        // Check for collisions
        RaycastHit2D[] hits = Physics2D.BoxCastAll(coll.bounds.center, coll.bounds.size, 0, Vector2.zero);
        Debug.Log("Hits: " + hits.Length);

        if (hits.Length > 0) {
            foreach(RaycastHit2D rc in hits) {
                Debug.Log("hit:" + rc.transform.name);
                // Check if the collision is interactable
                if (rc.IsInteractable()) {
                    rc.Interact();
                    // Return so that only the first interactable collision occurs
                    return;
                }
            }
        }

    }

    void OnSprint(InputValue value) {
        if (value.isPressed) {
            Debug.Log("Sprinting...");
            sprint = StartCoroutine(Sprint());
        } else {
            Debug.Log("No longer sprinting...");
            StopCoroutine(sprint);
            moveSpeed = walkSpeed;
        }
    }

    private IEnumerator Sprint() {
        while (true) {
            if (playerStaminaManager.enoughStamina(sprintCost)) {
                moveSpeed = sprintSpeed;
                playerStaminaManager.decreaseStamina(sprintCost);
            } else {
                moveSpeed = walkSpeed;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}
