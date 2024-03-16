using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Rigidbody2D RB;
    public Transform cam;
    public bool grounded = true;

    public float jumpCoolDown = 5f;
    float time = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider == null)
        {
            grounded = false;
        }
        if(col.collider != null){
            grounded = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal > 0.1 && grounded || horizontal < 0.1 && grounded)
        {
            RB.AddForce(cam.right * moveSpeed * horizontal);
        }

        if(horizontal > 0.1 && !grounded || horizontal < 0.1 && !grounded)
        {
            RB.AddForce(cam.right * moveSpeed * horizontal * 0.25f);   
        }

        if(!grounded)
        {
            RB.drag = 0.06f;
        }else{
            RB.drag = 1f;
        }

        if(vertical > 0.1 && grounded && jumpCoolDown < Time.time - time)
        {
            time = Time.time;
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.AddForce(cam.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jumped!");
        }
    }
}
