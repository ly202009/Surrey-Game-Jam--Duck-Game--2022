using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public int sceneInd;
    public GameObject player;
    bool updated = false;

    // Update is called once per frame
   void OnTriggerEnter2D(Collider2D col)
   {
       if(col.transform.name == player.name && !updated)
       {
            StartCoroutine(loadLevel(sceneInd));
            updated = true;
       }
   }

    IEnumerator loadLevel(int levelNum)
    {
        transition.SetTrigger("SwitchScene");
        Time.timeScale = 1;
        player.GetComponent<Movement>().SetCrumbs();
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(levelNum);
    }
}
