using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public int levelIndexNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     StartCoroutine(loadLevel(levelIndexNum));
        // }   
    }

    IEnumerator loadLevel(int levelNum)
    {
        transition.SetTrigger("SwitchScene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelNum);
    }
}
