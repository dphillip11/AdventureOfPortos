using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnImpact : MonoBehaviour
{
    public bool wasLaunched = false;
    public float launchPower;
    private Rigidbody2D rb;
    private Transform parent;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parent = transform.parent.transform;
        target = GameObject.Find("Portos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (wasLaunched)
        {
            var flightTime = calculateFlightTime();
            var velocityX = (target.position.x - transform.position.x) / flightTime;
            rb.simulated = true;
            rb.velocity = new Vector3(velocityX, launchPower, 0);
            StartCoroutine("cancelColliderTriggerStatus");
            wasLaunched = false;
        }
    }

    float calculateFlightTime()
    { 
        var s = target.position.y - transform.position.y;
        var u = launchPower;
        var a = Physics2D.gravity.y;

        var root1 = (-u + Mathf.Sqrt((u * u) + (2 * a * s))) / a;
        var root2 = (-u - Mathf.Sqrt((u * u) + (2 * a * s))) / a;

        if (root1 <= 0)
            return root2;
        else
            return root1;
   
    }

    private IEnumerator cancelColliderTriggerStatus()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<CapsuleCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Portos")
            GameObject.FindObjectOfType<HealthBarManager>().Damage(1);

        gameObject.SetActive(false);
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        rb.simulated = false;
        transform.localPosition = new Vector3(0.5f,0.5f,0);
        parent.GetComponent<Animator>().SetBool("hasAcorn", true);
    }
}
