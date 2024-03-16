using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public List<GameObject> clouds;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float minOffset;
    public float maxOffset;
    public float cloudMinSpeed;
    public float cloudMaxSpeed;
    public float limit;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCloud());
    }

    IEnumerator SpawnCloud()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Count)], new Vector2(transform.position.x, transform.position.y + Random.Range(minOffset, maxOffset)), Quaternion.identity);
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            cloud.transform.localScale = new Vector2(cloud.transform.localScale.x, cloud.transform.localScale.y);
        } else if(random == 1){
            cloud.transform.localScale = new Vector2(-cloud.transform.localScale.x, cloud.transform.localScale.y);
        }
        Rigidbody2D RB = cloud.GetComponent<Rigidbody2D>();
        RB.velocity = new Vector2(Random.Range(cloudMinSpeed, cloudMaxSpeed), 0);

        StartCoroutine(SpawnCloud());
    }
}
