using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCharacter : MonoBehaviour
{
    public Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButton("Fire1"))
        {
            RB.velocity = new Vector2(0, 0);
            RB.gravityScale = 0;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.MoveTowards(transform.position, mousePosition, 100 * Time.deltaTime);
        }

        if(Input.GetMouseButtonUp(0))
        {
            RB.gravityScale = 1;
        }

    }
}
