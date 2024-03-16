using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuitGame : MonoBehaviour, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
