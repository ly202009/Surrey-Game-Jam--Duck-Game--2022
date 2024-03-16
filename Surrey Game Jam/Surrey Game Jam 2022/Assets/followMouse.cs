using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    public Transform thing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButton("Fire1"))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            thing.position = mousePosition;
        }
        


    }
}
