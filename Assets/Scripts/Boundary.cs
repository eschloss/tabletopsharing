using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boundary : MonoBehaviour
{
    private bool boundariesOn = false;
    // Start is called before the first frame update
    void Start()
    {
        boundariesOn = Convert.ToBoolean(PlayerPrefs.GetInt("Toggle02"));
        if (!boundariesOn)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("boundary collision");
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Linked>().isBeingMoved && !obj.GetComponent<Linked>().isClicked)
        {
            obj.GetComponent<Linked>().Stop();
        }
    }
}
