using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortByY : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<SpriteRenderer>() != null)
        {
            sprite = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y);
    }
}
