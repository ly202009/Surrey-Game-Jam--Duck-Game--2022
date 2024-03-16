using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
    public int breadCrumbs = 0;
    public int health = 3;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchSpeed = 0.5f;
    public Vector2 minCameraBounds;
    public Vector2 maxCameraBounds;
    public float camOffset = 10f;
    public Rigidbody2D RB;
    public Transform cam;
    public LayerMask ignore;
    public Collider2D playerCol;
    public Collider2D slidingCol;
    public Collider2D duckedCol;
    public Animator transition;
    public GameObject menu;
    public TextMeshProUGUI score;
    public Transform background;
    public bool grounded = true;
    public bool sliding = false;
    public bool ducking = false;

    public Transform rayOrigin;
    public PhysicsMaterial2D slip;
    public PhysicsMaterial2D playerMat;

    public AudioSource audioSource;
    public GameObject walkingGameObj;
    public AudioClip quack;
    public AudioClip walk;
    AudioSource walkAudioSource;

    public float jumpCoolDown = 5f;

    public bool glide;
    public bool doubleJump;
    public bool jetpack;
    public bool gliding;
    public bool doubleJumping;

    public Animator animatorController;

    float time = 0f;
    float originalSpeed;
    private Vector2 velocity = Vector2.zero;
    Vector2 targetVelocity;
    bool secondJump = false;
    bool isShowing;
    
    // Start is called before the first frame update
    void Start()
    {
       originalSpeed = moveSpeed; 
       walkAudioSource = walkingGameObj.GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            StartCoroutine(Die());
        }
        if(Input.GetKey("r") || transform.position.y <= -20){
            StartCoroutine(Die());
        }
        if(transform.position.x <= maxCameraBounds.x && transform.position.x >= minCameraBounds.x)
        {
            cam.position = new Vector3(transform.position.x, cam.position.y, cam.position.z);
            background.position = new Vector3(transform.position.x, background.position.y, background.position.z);
        }
        if(transform.position.y <= maxCameraBounds.y && transform.position.y >= minCameraBounds.y)
        {
            cam.position = new Vector3(cam.position.x, transform.position.y + camOffset, cam.position.z);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 2f, ~ignore );

        if(jetpack)
        {
            grounded = true;
        }else if(hit.collider == null){
            grounded = false;
        }else{
            grounded = true;
            secondJump = true;
            RB.gravityScale = 5f;
            gliding = false;
        }
        if(Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
            if(isShowing)
            {
                Time.timeScale = 0;
            }else{
                Time.timeScale = 1;
            }
        }


        float crouchMultiplier = 1;

        Debug.DrawRay(transform.position, Vector2.up * 3f, Color.white);
        RaycastHit2D roofCheck = Physics2D.Raycast(transform.position, Vector2.up, 3f, ~ignore);

        if((Input.GetButtonDown("Duck") && RB.velocity.x != 0 && !ducking) || (sliding && (roofCheck.collider != null)))
        {
            ducking = false;
            sliding = true;
            slidingCol.isTrigger = false;
            playerCol.isTrigger = true;
            duckedCol.isTrigger = true;
            moveSpeed = originalSpeed;
            RB.freezeRotation = false;
            if(RB.velocity.x < 0.1 && RB.velocity.x > -0.1)
            {
                sliding = false;
                ducking = true;
            }
            // playerCol.sharedMaterial = slip;
            // playerCol.size = new Vector2(playerCol.size.x, 0.5f);
            // playerCol.size = new Vector2(1, originalSize.y/2);
            // playerCol.direction = (UnityEngine.CapsuleDirection2D)1;
        }else if((Input.GetButton("Duck") && RB.velocity.x < 0.1 && RB.velocity.x > -0.1) || (ducking && (roofCheck.collider != null))){
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
            ducking = true;
            sliding = false;
            duckedCol.isTrigger = false;
            slidingCol.isTrigger = true;
            playerCol.isTrigger = true;
            RB.freezeRotation = true;
            moveSpeed = 2.5f;
            
            // playerCol.sharedMaterial = playerMat;
            // playerCol.size = new Vector2(1, originalSize.y/2);
            // playerCol.direction = (UnityEngine.CapsuleDirection2D)1;
            crouchMultiplier = crouchSpeed;
        }

        if((ducking || sliding) && (roofCheck.collider == null) && !Input.GetButton("Duck"))
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
            ducking = false;
            sliding = false;
            playerCol.isTrigger = false;
            slidingCol.isTrigger = true;
            duckedCol.isTrigger = true;
            RB.freezeRotation = true;
            moveSpeed = originalSpeed;
            if(grounded)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            }
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        targetVelocity = new Vector2(horizontal * moveSpeed * 5 * crouchMultiplier, RB.velocity.y);
        if(RB.velocity.x < -1)
        {
            // gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.localScale = new Vector3(-2, transform.localScale.y, transform.localScale.z);
            
        }
        else if(RB.velocity.x > 1)
        {
            // gameObject.GetComponent<SpriteRenderer>().flipX = false;
            // transform.localScale = 2;
            transform.localScale = new Vector3(2, transform.localScale.y, transform.localScale.z);
        }

        if((RB.velocity.x < -1 || RB.velocity.x > 1) && !walkAudioSource.isPlaying && grounded && !sliding){
            walkAudioSource.PlayOneShot(walk, 1);
        }

        if(!sliding){
            // RB.AddForce(moveDirection.normalized * moveSpeed * 5);
            
            RB.velocity = Vector2.SmoothDamp(RB.velocity, targetVelocity, ref velocity, 0.3f);
            animatorController.SetFloat("Speed", Mathf.Abs(RB.velocity.x));

            if(!grounded)
            {
                RB.drag = 0.06f;
            }else{
                RB.drag = 1f;
            }

            
        }
        if(!sliding && !ducking)
        {
            if(Input.GetButton("Jump") && grounded && jumpCoolDown < Time.time - time)
            {
                time = Time.time;
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.AddForce(transform.up * jumpForce * 5, ForceMode2D.Impulse);
                audioSource.PlayOneShot(quack, 1);
            }
            if(Input.GetButtonDown("Jump") && doubleJump && secondJump && !grounded && jumpCoolDown < Time.time - time)
            {
                time = Time.time;
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.AddForce(transform.up * jumpForce * 5, ForceMode2D.Impulse);
                secondJump = false;
                audioSource.PlayOneShot(quack, 1);
            }else if(Input.GetButtonDown("Jump") && glide && secondJump && !grounded && jumpCoolDown < Time.time - time){
                RB.gravityScale = 1f;
                gliding = true;
                audioSource.PlayOneShot(quack, 1);
                playerCol.isTrigger = true;
                slidingCol.isTrigger = false;
                duckedCol.isTrigger = true;
            }
        }
        animatorController.SetBool("Glide", gliding);
        animatorController.SetBool("Ducked", ducking);
        animatorController.SetBool("InAir", !grounded);
        animatorController.SetBool("Slide", sliding);

        score.SetText(breadCrumbs.ToString("D4"));
    }

    public void AddCrumbs(int crumbs)
    {
        breadCrumbs += crumbs;
    }

    public void SetCrumbs()
    {
        StaticVars.breadCrumbs += breadCrumbs;
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
    }

    IEnumerator Die()
    {
        Time.timeScale = 1;
        transition.SetTrigger("SwitchScene");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
