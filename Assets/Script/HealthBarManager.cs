using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public List<GameObject> Hearts;
    public List<GameObject> HalfHearts;
    private int health = 8;
    public bool isDead = false;
    [SerializeField] private float invulnerabilityInterval = 0.5f;
    private bool isInvulnerable = false;
    // Start is called before the first frame update
    void Start()
    {
        DisplayHearts();
    }

    // Update is called once per frame
    void DisplayHearts()
    {
        for (int i = 0; i < HalfHearts.Count; i ++)
        {
            HalfHearts[i].SetActive(false);
        }
        var numberOfHearts = (int)(health / 2);
        for (int i = 0; i < Hearts.Count; i++)
        {
            if (i < numberOfHearts)
                Hearts[i].SetActive(true);
            else
                Hearts[i].SetActive(false);
        }
        if (health % 2 == 1)
            HalfHearts[numberOfHearts].SetActive(true);
    }

    public void Damage(int damage)
    {
        if (!isDead && !isInvulnerable)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
            }
            DisplayHearts();
            isInvulnerable= true;
            StartCoroutine("IgnoreDamage");
        }
    }

    private IEnumerator IgnoreDamage()
    {
        yield return new WaitForSeconds(invulnerabilityInterval);
        isInvulnerable = false;
    }
}
