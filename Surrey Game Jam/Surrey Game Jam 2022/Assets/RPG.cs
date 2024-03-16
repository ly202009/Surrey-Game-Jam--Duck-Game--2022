using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : MonoBehaviour
{
    public Sprite rocket;
    public GameObject rocketInactive;
    public GameObject rocketActive;
    public GameObject player;
    public Transform gunEnd;
    public Vector3 posOffset;
    public float gunOffset;

    public float fireSpeed;

    public AudioClip rocketSound;
    public AudioSource audioSource;

    float time;
    bool right;
    float rotation_z;

    Vector3 angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ray ray = new Ray(gunEnd.position, new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
        // Ray ray = new Ray(gunEnd.position, new Vector3(0, 0, gunEnd.rotation.z));
        // RaycastHit2D hit = Physics2d;
        // Debug.DrawRay(gunEnd.position, new Vector3(0, 0, gunEnd.rotation.z), Color.white);
        var checkGun = Physics2D.OverlapCircle(gunEnd.position, 0.1f);

        if(Time.timeScale != 0)
        {
            if(Time.time - time > fireSpeed)
            {
                rocketInactive.GetComponent<SpriteRenderer>().sprite = rocket;
            }else{
                rocketInactive.GetComponent<SpriteRenderer>().sprite = null;
            }

            if(Input.GetButton("Fire1") && Time.time - time > fireSpeed && checkGun == null)
            {
                time = Time.time;
                Fire();
            }
            // if(hit.collider != null)
            // {
            //     Debug.Log(hit.collider.gameObject.name);
            // }
            // if(Physics2D.Raycast(ray, out hit))
            // {
            //     Debug.Log(hit.collider.gameObject.name);
            // }

            transform.position = player.transform.position + posOffset;
            angle = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle.Normalize();
            rotation_z = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;


            if(!right){
                if((rotation_z > 80f && rotation_z <= 180) || (rotation_z > -180 && rotation_z < -80))
                {   
                    right = false;
                    transform.rotation = Quaternion.Euler(0, 0, rotation_z);
                    transform.localScale = new Vector2(transform.localScale.x, -2);
                } else {
                    right = true;
                    transform.rotation = Quaternion.Euler(0, 0, rotation_z);
                    transform.localScale = new Vector2(transform.localScale.x, 2);
                }
            } else {
                if((rotation_z > 100f && rotation_z <= 180) || (rotation_z > -180 && rotation_z < -100))
                {   
                    right = false;
                    transform.rotation = Quaternion.Euler(0, 0, rotation_z);
                    transform.localScale = new Vector2(transform.localScale.x, -2);
                } else {
                    right = true;
                    transform.rotation = Quaternion.Euler(0, 0, rotation_z);
                    transform.localScale = new Vector2(transform.localScale.x, 2);
                }
            }
        }

    }

    void Fire()
    {
        audioSource.PlayOneShot(rocketSound, 1);

        Instantiate(rocketActive, gunEnd.position, Quaternion.Euler(0, 0, rotation_z));
    }
}
