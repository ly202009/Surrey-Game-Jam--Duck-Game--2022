using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    public bool left;
    public bool right;
    public List<GameObject> cars;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float carSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        GameObject car = Instantiate(cars[Random.Range(0, cars.Count)], transform.position, Quaternion.identity);
        if(left)
        {
            car.transform.localScale = new Vector2(car.transform.localScale.x, car.transform.localScale.y);
        } else if(right){
            car.transform.localScale = new Vector2(-car.transform.localScale.x, car.transform.localScale.y);
        }
        Rigidbody2D RB = car.GetComponent<Rigidbody2D>();
        RB.velocity = new Vector2(carSpeed * Mathf.Sign(car.transform.localScale.x), 0);

        StartCoroutine(SpawnCar());
    }
}
