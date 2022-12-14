using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private HealthBarManager health;
    [SerializeField] float recoilForce = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        health = GameObject.FindObjectOfType<HealthBarManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hedgehog"))
        {
            Debug.Log(collision.contacts[0].normal);
            if (collision.contacts[0].normal.y > -0.5)
            {
                health.Damage(1);
                rb.AddForce(collision.contacts[0].normal * recoilForce, ForceMode2D.Impulse);
            }
        }
    }

}
