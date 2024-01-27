using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    public bool onGround;
    public bool canJump;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        // Horizontal Movement
        float xMovement = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * xMovement * moveSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Checks if the player is standing on ground or similar
    {
        if (collision.gameObject.CompareTag("StandableGround") &&
            transform.position.y > collision.transform.position.y)
        {
            onGround = true;
            canJump = true;

            if (Input.GetKey(KeyCode.Space)) // Allows for holding down the jump button to continuously jump
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
                onGround = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StandableGround"))
        {
            StartCoroutine(CoyoteTime());
            onGround = false;
        }
    }

    private IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(0.125f);
        if (!onGround)
        {
            canJump = false;
        }
    }
}
