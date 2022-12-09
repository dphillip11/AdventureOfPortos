using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    private Transform cameraTransform;
    private float loopingPointX;
    [SerializeField] private float yLimit;
    [SerializeField] float parallaxRatioX;
    [SerializeField] float parallaxRatioY;
    [SerializeField] float zPos;
    private float startingX;
    private float startingY;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        loopingPointX = GetComponent<BoxCollider2D>().size.x;
        startingX = transform.position.x;
        startingY = transform.position.y;
        zPos = transform.position.z;

    }
        // Update is called once per frame
    void Update()
    {
        var x = cameraTransform.position.x + startingX + (cameraTransform.position.x * parallaxRatioX) % loopingPointX;
        var y = cameraTransform.position.y + Mathf.Max(-yLimit, Mathf.Min(yLimit, startingY + (cameraTransform.position.y * parallaxRatioY)));
        transform.position = new Vector3(x, y, zPos);
    }
}
