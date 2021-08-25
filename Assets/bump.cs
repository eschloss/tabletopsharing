using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bump : MonoBehaviour
{
    public GameObject bumpedObj;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("bump collision");
        GameObject obj = other.gameObject;
        if (!obj.GetComponent<Linked>().isBeingMoved)
        {
            Debug.Log("bump collision2");
            bumpedObj = obj;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        bumpedObj = null;
    }

}
