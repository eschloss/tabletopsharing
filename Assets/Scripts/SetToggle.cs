using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToggle : MonoBehaviour
{
    void Start()
    {
        this.SavePref();
    }
    
    public void SavePref()
    {
        bool isOn = this.gameObject.GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt(this.gameObject.name, isOn ? 1 : 0);
    }
}
