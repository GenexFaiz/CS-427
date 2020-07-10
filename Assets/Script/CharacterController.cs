using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigid;
    private Animator anim;

    public float moveSpeed;
    public float directX;
    public bool facingRight;
    private Vector3 localScale;
    private float jumpForce;

    private bool isJumping;

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 5f;
        jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        print(rigid.velocity.y);
        directX = Input.GetAxisRaw("Horizontal")*moveSpeed;

        if(Input.GetButtonDown("Jump") && rigid.velocity.y < 0.1 && rigid.velocity.y > -0.1) {
            rigid.AddForce(Vector2.up * jumpForce * rigid.mass, ForceMode2D.Impulse);
        }

        if (Mathf.Abs(directX) > 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
        if (rigid.velocity.y < 0.1 && rigid.velocity.y > -0.1)
            anim.SetBool("isJumping", false);
        else
            anim.SetBool("isJumping", true);
    }

    private void FixedUpdate() {
        rigid.velocity = new Vector2(directX, rigid.velocity.y);
    }

    private void LateUpdate() {
        if (directX > 0) facingRight = true;
        else if (directX < 0) facingRight = false;

        if ((facingRight && localScale.x < 0) || (!facingRight && localScale.x > 0))
            localScale.x *= -1;
        transform.localScale = localScale;
    }
}
