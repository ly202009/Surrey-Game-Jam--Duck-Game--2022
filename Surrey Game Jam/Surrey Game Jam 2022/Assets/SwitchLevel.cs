using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour, IPointerUpHandler
{
    public Animator transition;
    public float transitionTime;
    public int levelIndexNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(loadLevel(levelIndexNum));
    }

    IEnumerator loadLevel(int levelNum)
    {
        transition.SetTrigger("SwitchScene");
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(levelNum);
    }
}
