using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnImpact : MonoBehaviour
{
    public bool wasLaunched = false;
    public float launchPowerX;
    public float launchPowerY;
    public Vector3 acornOffset = new Vector3(0.2f, -0.2f, 0);
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
            var xSpeed = calculateVelocityX();
            xSpeed = Mathf.Max(Mathf.Min(xSpeed, launchPowerX), -launchPowerX);
            
            if (xSpeed != float.NaN)
            {
                LaunchAcorn(xSpeed);
            }
        }
    }

    private void LaunchAcorn(float velocityX)
    {
        rb.simulated = true;
        rb.velocity = new Vector3(velocityX, launchPowerY, 0);
        StartCoroutine("cancelColliderTriggerStatus");
        wasLaunched = false;
    }

    private float calculateFlightTime()
    { 
        var s = target.position.y - transform.position.y;
        var u = launchPowerY;
        var a = Physics2D.gravity.y;

        //var root1 = (-u + Mathf.Sqrt((u * u) + (2 * a * s))) / a;
        var root2 = (-u - Mathf.Sqrt((u * u) + (2 * a * s))) / a;

        return root2;
    }

    private float calculateVelocityX()
    {
        var flightTime = calculateFlightTime();
        var velocityX = (target.position.x - transform.position.x) / flightTime;
        return velocityX;
    }

    private IEnumerator cancelColliderTriggerStatus()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(3);
        ResetAcorn();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Portos")
            GameObject.FindObjectOfType<HealthBarManager>().Damage(1);
        ResetAcorn();
        
    }

    private void ResetAcorn()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        rb.simulated = false;
        transform.localPosition = acornOffset;
        parent.GetComponent<Animator>().SetBool("hasAcorn", true);
    }

    private void OnBecameInvisible()
    {
        ResetAcorn();
    }

    
}
