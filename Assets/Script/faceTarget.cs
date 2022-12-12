using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceTarget : MonoBehaviour
{
    private Transform target;
    private float scale;
    private float width;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Portos").transform;
        tf = transform;
        scale = Mathf.Abs(tf.localScale.x);
        width = GetComponent<CapsuleCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.x < tf.position.x - (width / 2))
            tf.localScale = new Vector3(-scale, scale, scale);
        if (target.transform.position.x > tf.position.x + (width / 2))
            tf.localScale = new Vector3(scale, scale, scale);
    }
}
