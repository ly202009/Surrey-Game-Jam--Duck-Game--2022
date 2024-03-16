using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeapon : MonoBehaviour
{
    public GameObject glock;
    public GameObject ak;
    public GameObject RPG;
    public Transform duck;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
        if(StaticVars.glock){
            obj = Instantiate(glock, duck.position, Quaternion.identity);

        }else if(StaticVars.ak){
            obj = Instantiate(ak, duck.position, Quaternion.identity);

        }else if(StaticVars.RPG){
            obj = Instantiate(RPG, duck.position, Quaternion.identity);

        }

        if(obj.GetComponent<Gun>() != null)
        {
            obj.GetComponent<Gun>().player = duck.gameObject;

        }else if(obj.GetComponent<RPG>() != null)
        {
            obj.GetComponent<RPG>().player = duck.gameObject;
        }
        
    }
}
