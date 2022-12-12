using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelEscape : MonoBehaviour
{
    [SerializeField] private float playerChaseRange = 5;
    private Animator anim;
    private Transform player;
    private bool isEscaping = false;
    private float turnBuffer = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Portos").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check in chasing range
        if (Vector2.Distance(transform.position, player.position) < playerChaseRange)
        {
            anim.SetBool("isEscaping", true);
            isEscaping = true;
            if (player.position.x < transform.position.x - turnBuffer)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            if (player.position.x > transform.position.x + turnBuffer)
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            anim.SetBool("isEscaping", false);
            isEscaping = false;
            if (player.position.x < transform.position.x - turnBuffer)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            if (player.position.x > transform.position.x + turnBuffer)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down);
        if (raycastHit.collider != null)
            anim.SetBool("hasJumped", true);
    }

}
