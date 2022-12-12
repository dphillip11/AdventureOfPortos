
using UnityEngine;

public class SpawnMysteryItem : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] Color color;
    private bool wasUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.75)
        {
            if (!wasUsed)
            {
                var reward = prefabs[Random.Range(0, prefabs.Length)];
                Instantiate(reward, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
                GetComponent<SpriteRenderer>().color = color;
                wasUsed = true;
            }
        }
    
}

}