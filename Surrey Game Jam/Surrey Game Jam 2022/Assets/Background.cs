using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform background;

    void Update()
    {
        background.position = new Vector3(transform.position.x, transform.position.y, background.position.z);
    }
}
