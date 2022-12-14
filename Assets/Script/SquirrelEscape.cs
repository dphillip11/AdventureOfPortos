using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelEscape : MonoBehaviour
{
    [SerializeField] private float playerChaseRange = 5;
    private Animator anim;
    private Transform player;
    private float turnBuffer = 0.4f;
    public ParticleSystem feathers;
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
            if (player.position.x < transform.position.x - turnBuffer)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            if (player.position.x > transform.position.x + turnBuffer)
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            anim.SetBool("isEscaping", false);
            if (player.position.x < transform.position.x - turnBuffer)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            if (player.position.x > transform.position.x + turnBuffer)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Portos")
        {
            Instantiate(feathers, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down);
            if (raycastHit.collider != null)
                anim.SetBool("hasJumped", true);
        }
    }

}
