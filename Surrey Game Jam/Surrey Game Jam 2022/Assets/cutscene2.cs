using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutscene2 : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public float sceneTime;
    bool updated = false;

    void Awake()
   {
       if(!updated)
       {
            StartCoroutine(loadLevel(2));
            updated = true;
       }
   }

    IEnumerator loadLevel(int levelNum)
    {
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(sceneTime);
        transition.SetTrigger("SwitchScene");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(levelNum);
    }
}
