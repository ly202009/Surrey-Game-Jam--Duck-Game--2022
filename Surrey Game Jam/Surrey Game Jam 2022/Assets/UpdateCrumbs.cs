using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateCrumbs : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.SetText("Bread Crumbs: " + StaticVars.breadCrumbs);
    }
}
