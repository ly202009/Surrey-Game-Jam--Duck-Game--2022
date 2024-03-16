using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour, IPointerUpHandler
{
    public Animator transition;
    public float transitionTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator loadLevel(int levelNum)
    {
        Time.timeScale = 1;
        transition.SetTrigger("SwitchScene");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(levelNum);
    }
}
