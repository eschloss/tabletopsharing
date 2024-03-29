using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributedMirror : MonoBehaviour
{

    public GameObject inverseMirror;
    public GameObject table;
    List <GameObject> currentCollisions = new List <GameObject> ();
	public GameObject decalPrefab;

    void Update()
    {
        foreach (GameObject obj in currentCollisions)
        {
            Linked linked = obj.GetComponent<Linked>();
            if (linked.doubleClickTrigger && !linked.obj)
            {
                Mirror(obj);
            } else if (!linked.doubleClickTrigger && linked.obj)
            {
                Unmirror(obj);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject origObj = other.gameObject;
        if (origObj.GetComponent<Linked>() != null)
        {
            currentCollisions.Add(origObj);
        }
    }

    void Unmirror(GameObject origObj)
    {
        Linked linked = origObj.GetComponent<Linked>();
        Destroy(linked.obj);
        linked.obj = null;
        
        GameObject origHighlight = origObj.transform.Find("highlight").gameObject;
        origHighlight.SetActive(false);
    }

    void Mirror(GameObject origObj) {
        if (origObj.GetComponent<Linked>().isBeingMoved)
        {
            var mirroredObj = Instantiate(decalPrefab);
            mirroredObj.transform.parent = table.transform;
            SpriteRenderer mo_spriteRenderer = mirroredObj.GetComponent<SpriteRenderer>();
            mo_spriteRenderer.color = origObj.GetComponent<SpriteRenderer>().color;
            mo_spriteRenderer.sprite = origObj.GetComponent<SpriteRenderer>().sprite;
            mo_spriteRenderer.drawMode = origObj.GetComponent<SpriteRenderer>().drawMode;
            mirroredObj.GetComponent<Linked>().doubleClickTrigger = true;
            mirroredObj.name = origObj.name;
            mirroredObj.transform.localPosition = new Vector3(-origObj.transform.localPosition.x,
                -origObj.transform.localPosition.y, origObj.transform.localPosition.z); 
            mirroredObj.transform.localScale = origObj.transform.localScale;
            
            GameObject origHighlight = origObj.transform.Find("highlight").gameObject;
            origHighlight.SetActive(true);
            GameObject newHighlight = mirroredObj.transform.Find("highlight").gameObject;
            newHighlight.SetActive(true);
            
            mirroredObj.GetComponent<Linked>().obj = origObj;
            origObj.GetComponent<Linked>().obj = mirroredObj;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject origObj = other.gameObject;
        if (origObj.GetComponent<Linked>() != null)
        {
            currentCollisions.Remove(origObj);
        
            if (origObj.GetComponent<Linked>().isBeingMoved)
            {
                Unmirror(origObj);
            }
        }
    }
}
