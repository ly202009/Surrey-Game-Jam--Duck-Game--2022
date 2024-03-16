using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetWeaponGlock : MonoBehaviour, IPointerUpHandler
{
    public GameObject akButton;
    public GameObject glockButton;
    public GameObject RPGButton;
    public bool thisWasLast = true;
    void Start()
    {
        glockButton.GetComponent<Button>().Select();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StaticVars.glock = true;
        StaticVars.ak = false;
        StaticVars.RPG = false;
        thisWasLast = true;
        akButton.GetComponent<SetWeaponAK>().thisWasLast = false;
        RPGButton.GetComponent<SetWeaponRPG>().thisWasLast = false;
        glockButton.GetComponent<Button>().Select();

    }

    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != akButton && EventSystem.current.currentSelectedGameObject != glockButton && EventSystem.current.currentSelectedGameObject != RPGButton && thisWasLast)
        {
            glockButton.GetComponent<Button>().Select();
        }
    }
}
