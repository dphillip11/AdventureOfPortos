using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateChildObject : MonoBehaviour
{
    public GameObject child;

    public void ActivateChild()
    {
       child.SetActive(true); 
    }

    public void DeActivateChild()
    {
        child.SetActive(false);
    }
}
