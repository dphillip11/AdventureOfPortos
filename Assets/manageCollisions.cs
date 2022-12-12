using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageCollisions : MonoBehaviour
{
    CinemachineImpulseSource impulseSource;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (anim.GetBool("Jumping"))
        {
            impulseSource.GenerateImpulse();
            anim.SetBool("Jumping", false);
            if (collision.gameObject.name == "Portos")
            {
                var normal = collision.GetContact(0).normal;
                Debug.Log(normal.ToString());
            }
        }
    }
}
