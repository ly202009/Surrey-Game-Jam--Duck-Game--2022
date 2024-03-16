using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetWeaponRPG : MonoBehaviour, IPointerUpHandler
{
    public Button akButton;
    public Button glockButton;
    public Button RPGButton;
    public GameObject priceTag;
    public int price;
    public bool thisWasLast;
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(StaticVars.breadCrumbs >= price && !StaticVars.RPGUnlocked)
        {
            StaticVars.RPGUnlocked = true;
            StaticVars.breadCrumbs -= price;
            Destroy(priceTag);
            thisWasLast = true;
        }
        if(StaticVars.RPGUnlocked)
        {
            StaticVars.glock = false;
            StaticVars.ak = false;
            StaticVars.RPG = true;
            thisWasLast = true;
            akButton.gameObject.GetComponent<SetWeaponAK>().thisWasLast = false;
            glockButton.gameObject.GetComponent<SetWeaponGlock>().thisWasLast = false;
        }else{
            if(akButton.gameObject.GetComponent<SetWeaponAK>().thisWasLast)
            {
                akButton.Select();
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
        if(StaticVars.RPGUnlocked)
        {
            Destroy(priceTag);
        }
    }
}
