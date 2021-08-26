using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror : MonoBehaviour
{

    public GameObject inverseMirror;
    public GameObject table;
	public GameObject decalPrefab;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject origObj = other.gameObject;
        if (origObj.GetComponent<Linked>().isBeingMoved)
        {
            var mirroredObj = Instantiate(decalPrefab);
            mirroredObj.transform.parent = table.transform;
            SpriteRenderer mo_spriteRenderer = mirroredObj.GetComponent<SpriteRenderer>();
            mo_spriteRenderer.color = origObj.GetComponent<SpriteRenderer>().color;
            mo_spriteRenderer.sprite = origObj.GetComponent<SpriteRenderer>().sprite;
            mo_spriteRenderer.drawMode = origObj.GetComponent<SpriteRenderer>().drawMode;
            mirroredObj.transform.localPosition = new Vector3(-origObj.transform.localPosition.x,
                -origObj.transform.localPosition.y, origObj.transform.localPosition.z); 
            mirroredObj.transform.localScale = origObj.transform.localScale;
            mirroredObj.name = origObj.name;

            mirroredObj.GetComponent<Linked>().obj = origObj;
            origObj.GetComponent<Linked>().obj = mirroredObj;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Linked>().isBeingMoved)
        {
            Linked linked = other.gameObject.GetComponent<Linked>();
            Destroy(linked.obj);
            linked.obj = null;
            Debug.Log("Collision Exit");
        }
    }
}
