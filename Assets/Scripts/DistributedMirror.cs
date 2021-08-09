using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributedMirror : MonoBehaviour
{

    public GameObject inverseMirror;
    public GameObject table;
    List <GameObject> currentCollisions = new List <GameObject> ();

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
            GameObject mirroredObj = new GameObject();
            mirroredObj.transform.parent = table.transform;
            mirroredObj.AddComponent<Rigidbody2D>();
            mirroredObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            mirroredObj.AddComponent<BoxCollider2D>();
            mirroredObj.transform.localPosition = new Vector3(-origObj.transform.localPosition.x,
                -origObj.transform.localPosition.y, origObj.transform.localPosition.z); 
            mirroredObj.transform.localScale = origObj.transform.localScale;
            mirroredObj.AddComponent<SpriteRenderer>();
            SpriteRenderer mo_spriteRenderer = mirroredObj.GetComponent<SpriteRenderer>();
            mo_spriteRenderer.color = origObj.GetComponent<SpriteRenderer>().color;
            mo_spriteRenderer.sprite = origObj.GetComponent<SpriteRenderer>().sprite;
            mirroredObj.name = origObj.name + " Copy";
            mirroredObj.AddComponent<Linked>();
            mirroredObj.GetComponent<Linked>().obj = origObj;
            mirroredObj.AddComponent<FlickDeceleration>();
            mirroredObj.GetComponent<Linked>().doubleClickTrigger = true;

            /***Highlight copy ***/
            GameObject origHighlight = origObj.transform.Find("highlight").gameObject;
            origHighlight.SetActive(true);
            GameObject mirroredHighlight = new GameObject();
            mirroredHighlight.transform.parent = mirroredObj.transform;
            mirroredHighlight.transform.localPosition = new Vector3(origHighlight.transform.localPosition.x,
                origHighlight.transform.localPosition.y, origHighlight.transform.localPosition.z); 
            mirroredHighlight.transform.localScale = origHighlight.transform.localScale;
            mirroredHighlight.AddComponent<SpriteRenderer>();
            SpriteRenderer mh_spriteRenderer = mirroredHighlight.GetComponent<SpriteRenderer>();
            mh_spriteRenderer.color = origHighlight.GetComponent<SpriteRenderer>().color;
            mh_spriteRenderer.sprite = origHighlight.GetComponent<SpriteRenderer>().sprite;
            mirroredHighlight.name = "highlight";

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
