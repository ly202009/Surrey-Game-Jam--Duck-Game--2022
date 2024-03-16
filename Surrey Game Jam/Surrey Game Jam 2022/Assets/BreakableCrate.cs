using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCrate : MonoBehaviour
{
    public int minValue = 1;
    public int maxValue = 10;
    public Object broken;
    public int health = 1; 
    public bool killedByEnemy = false;
    public GameObject player;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y);
        if(health <= 0)
        {
            if(!killedByEnemy){
                player.GetComponent<Movement>().AddCrumbs(Random.Range(minValue, maxValue + 1));
            }
            GameObject gameObj = (GameObject)Instantiate(broken, transform.position, transform.rotation);
            gameObj.transform.localScale = new Vector2(transform.localScale.x/2, transform.localScale.y/2);
            Destroy(gameObject);
        }
    }
}
