using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror : MonoBehaviour
{

    public GameObject inverseMirror;
    public GameObject table;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CollisionStart");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject origObj = other.gameObject;
        if (origObj.GetComponent<Linked>().isClicked)
        {
            Debug.Log("Collision Enter");
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

            origObj.GetComponent<Linked>().obj = mirroredObj;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Linked>().isClicked)
        {
            Linked linked = other.gameObject.GetComponent<Linked>();
            Destroy(linked.obj);
            linked.obj = null;
            Debug.Log("Collision Exit");
        }
    }
}
