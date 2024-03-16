using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public Transform gunEnd;
    public Vector3 posOffset;
    public float gunOffset;
    public GameObject sparks;
    public Sprite muzzleFlash;
    public float muzzleEffect;
    public float spread;

    public float fireSpeed;

    public AudioClip gunSound;
    public AudioSource audioSource;

    float time;
    SpriteRenderer muzzle;
    bool right;
    bool gliding;

    Vector3 angle;

    // Start is called before the first frame update
    void Start()
    {
        muzzle = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        muzzle.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Ray ray = new Ray(gunEnd.position, new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
        // Ray ray = new Ray(gunEnd.position, new Vector3(0, 0, gunEnd.rotation.z));
        // RaycastHit2D hit = Physics2d;
        // Debug.DrawRay(gunEnd.position, new Vector3(0, 0, gunEnd.rotation.z), Color.white);

        gliding = player.GetComponent<Movement>().gliding;
        var checkGun = Physics2D.OverlapCircle(gunEnd.position, 0.1f);

        if(Time.timeScale != 0)
        {
            if(Input.GetButton("Fire1") && Time.time - time > fireSpeed && checkGun == null && !gliding)
            {
                time = Time.time;
                Shoot();
                muzzle.sprite = muzzleFlash;

            }else if (Time.time - time > muzzleEffect){
                muzzle.sprite = null;
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
            float rotation_z = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;


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

    void Shoot()
    {
        audioSource.PlayOneShot(gunSound, 1);
        
        gunEnd.localRotation = Quaternion.Euler(0, 0, Random.Range(-spread, spread));
        RaycastHit2D hit = Physics2D.Raycast(gunEnd.position, gunEnd.right);

        if(hit)
        {
            Instantiate(sparks, hit.point, Quaternion.LookRotation(hit.normal));
            if(hit.transform.GetComponent<Break>() != null)
            {
                hit.transform.GetComponent<Break>().health -= 1;
            }

            if(hit.transform.GetComponent<BreakableCrate>() != null)
            {
                hit.transform.GetComponent<BreakableCrate>().health -= 1;
            }

            if(hit.transform.GetComponent<EnemyAI>() != null)
            {
                hit.transform.GetComponent<EnemyAI>().patrolling = true;
            }
        }
    }
}
