using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float delay = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
