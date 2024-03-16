using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    bool touchingPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.name == player.name)
        {
            touchingPlayer = true;
        }else{
            touchingPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && !touchingPlayer)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }
}
