using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject player;
    public ParticleSystem sparks;
    public Rigidbody2D RB;
    public Sprite muzzleFlash;
    RaycastHit2D left;
    RaycastHit2D right;
    RaycastHit2D leftDown;
    RaycastHit2D rightDown;
    public bool seeingPlayer = false;
    public bool patrolling = false;
    public bool detectEdge = true;
    public float moveSpeed = 5f;
    public float fireRate;
    public float fireDelay;
    public float muzzleEffect;
    public LayerMask ignore;
    public AudioClip shotSound;
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioSource walkAudioSource;
    public Animator anim;

    float time = 0f;
    bool seenPlayer = false;
    bool finishedRunning = true;
    SpriteRenderer spriteRenderer;
    Vector2 targetVelocity;
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = shootPoint.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
        anim.SetFloat("Speed", Mathf.Abs(RB.velocity.x));

        if(Mathf.Abs(RB.velocity.x) > 0.1f && !walkAudioSource.isPlaying)
        {
            walkAudioSource.PlayOneShot(walkSound, 1);
        }

        left = Physics2D.Raycast(transform.position, -transform.right, 2f, ~ignore);
        right = Physics2D.Raycast(transform.position, transform.right, 2f, ~ignore);
        leftDown = Physics2D.Raycast(transform.position, -transform.right - transform.up, 2, ~ignore);
        rightDown = Physics2D.Raycast(transform.position, transform.right - transform.up, 2, ~ignore);

        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right * Mathf.Sign(transform.localScale.x));
        targetVelocity = new Vector2(transform.localScale.x * moveSpeed * 5, RB.velocity.y);

            if(hit.collider != null && hit.transform.name == player.name)
            {
                seeingPlayer = true;
            }else{
                seeingPlayer = false;
            }
            

        if(patrolling && !seeingPlayer)
        {
            if(left.collider != null || (leftDown.collider == null && detectEdge))
            {
                transform.localScale = new Vector2(2, transform.localScale.y);
            }else if(right.collider != null || (rightDown.collider == null && detectEdge)){
                transform.localScale = new Vector2(-2, transform.localScale.y);
            }
            RB.velocity = Vector2.SmoothDamp(RB.velocity, targetVelocity, ref velocity, 0.3f);
        }
        
        if(seeingPlayer && Time.time - time >= fireRate && finishedRunning)
        {
            StartCoroutine(Fire());
        }else if(Time.time - time >= muzzleEffect){
            spriteRenderer.sprite = null;
        }
    }
    // 
    IEnumerator Fire()
    {   
        finishedRunning = false;
        if(!seenPlayer && seeingPlayer)
        {
           yield return new WaitForSeconds(fireDelay);
           seenPlayer = true;
        } else if(!seenPlayer && seeingPlayer)
        {
           yield return new WaitForSeconds(fireDelay/2);
           seenPlayer = true;
        }
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right * Mathf.Sign(transform.localScale.x));
        spriteRenderer.sprite = muzzleFlash;
        audioSource.PlayOneShot(shotSound, 1);
        if(hit.collider != null)
        {
            Instantiate(sparks, hit.point, Quaternion.LookRotation(hit.normal));
            if(hit.transform.GetComponent<Movement>() != null)
            {
                hit.transform.GetComponent<Movement>().LoseHealth(1);
            }
            if(hit.transform.GetComponent<BreakableCrate>() != null)
            {
                hit.transform.GetComponent<BreakableCrate>().health -= 1;
                hit.transform.GetComponent<BreakableCrate>().killedByEnemy = true;
            }
        }
        finishedRunning = true;
        time = Time.time;
        yield return null;
    }
}