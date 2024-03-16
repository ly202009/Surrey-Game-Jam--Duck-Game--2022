using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public int force;
    public float speed;
    public float impactRadius;
    public LayerMask effected;
    public GameObject explosion;
    public Rigidbody2D RB;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 15);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Boom();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(ignore.value);
        Physics2D.IgnoreLayerCollision(6, 8);
        RB.AddForce(speed * transform.right);
    }

    void Boom()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactRadius, effected);
        foreach(Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            if(obj.GetComponent<Rigidbody2D>() != null)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            }
            if(obj.GetComponent<BreakableCrate>() != null)
            {
                obj.GetComponent<BreakableCrate>().health -= 5;
            }
            
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
