using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HedgehogPatrol : MonoBehaviour
{
    [SerializeField] private float playerChaseRange = 5;
    [SerializeField] private float chasingAnimationSpeed = 1.5f;
    [SerializeField] private float rayLength = 1;
    [SerializeField] private Transform raycastAnchor;
    [SerializeField] private float walkSpeed = 1.5f;
    private float turnBuffer = 0.4f;
    private Animator anim;
    private Transform player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Portos").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.2f)
        {
            // check in chasing range
            if (Vector2.Distance(transform.position, player.position) < playerChaseRange)
                Chase();
            else
                Patrol();
        }
    }

    private bool CheckForGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(raycastAnchor.position, Vector2.down, rayLength);
        Debug.DrawRay(raycastAnchor.position, Vector2.down * rayLength);
            if (raycastHit.collider == null)
                return false;
        return true;

    }

    private void Chase()
    {
        anim.speed = chasingAnimationSpeed;
        if (player.position.x < transform.position.x - turnBuffer)
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        if (player.position.x > transform.position.x + turnBuffer)
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        rb.velocity = Vector2.right * walkSpeed * chasingAnimationSpeed * transform.localScale.x;
    }

    private void Patrol()
    {
        anim.speed = 1;
        if (!CheckForGround())
        {
            //change direction to avoid falling
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        //move forward relative to scale
        rb.velocity = Vector2.right * walkSpeed * transform.localScale.x;
    }
}
