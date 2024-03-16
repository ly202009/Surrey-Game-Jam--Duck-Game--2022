using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetWeaponAK : MonoBehaviour, IPointerUpHandler
{
    public Button akButton;
    public Button glockButton;
    public Button RPGButton;
    public GameObject priceTag;
    public int price;
    public bool thisWasLast;

    public void OnPointerUp(PointerEventData eventData)
    {
        if(StaticVars.breadCrumbs >= price && !StaticVars.akUnlocked)
        {
            StaticVars.akUnlocked = true;
            StaticVars.breadCrumbs -= price;
            Destroy(priceTag);
            thisWasLast = true;
        }

        if(StaticVars.akUnlocked)
        {
            StaticVars.glock = false;
            StaticVars.ak = true;
            StaticVars.RPG = false;
            thisWasLast = true;
            glockButton.gameObject.GetComponent<SetWeaponGlock>().thisWasLast = false;
            RPGButton.gameObject.GetComponent<SetWeaponRPG>().thisWasLast = false;
        }else{
            if(RPGButton.gameObject.GetComponent<SetWeaponRPG>().thisWasLast)
            {
                RPGButton.Select();
            }else{
                glockButton.Select();
            }
        }
    }
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != akButton && EventSystem.current.currentSelectedGameObject != glockButton && EventSystem.current.currentSelectedGameObject != RPGButton && thisWasLast)
        {
            gameObject.GetComponent<Button>().Select();
        }
    }
    void Awake()
    {
        if(StaticVars.akUnlocked)
        {
            Destroy(priceTag);
        }
    }
}
