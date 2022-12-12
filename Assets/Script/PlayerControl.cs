using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool onGround = false;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputHorizontal = Input.GetAxis("Horizontal");
        if (inputHorizontal != 0)
        {
            animator.SetBool("isRunning", true);
            if (inputHorizontal < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (inputHorizontal > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
            animator.SetBool("isRunning", false);

        transform.Translate(Vector3.right * inputHorizontal * speed * Time.deltaTime);

        if (Mathf.Abs(rb.velocity.y) < -0.1f && onGround)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y), Vector2.down * 0.2f);
            Debug.DrawRay(new Vector2(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y), Vector2.down * 0.2f);
            if (raycastHit.collider == null)
            {
                animator.SetBool("isOnGround", false);
                onGround = false;
            }
        }
        if (onGround && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
            animator.SetBool("isOnGround", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y), Vector2.down * 0.2f);
        Debug.DrawRay(new Vector2(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y), Vector2.down * 0.2f);
        if (raycastHit.collider != null)
        { 
            animator.SetBool("isOnGround", true);
            onGround = true;
        }
    }



}
