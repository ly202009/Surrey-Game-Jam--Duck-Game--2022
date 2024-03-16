using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public float boundMin;
    public float boundMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > boundMax || transform.position.x < boundMin)
        {
            Destroy(gameObject);
        }
    }
}
