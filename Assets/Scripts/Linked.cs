using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linked : MonoBehaviour
{
    public GameObject obj;
    public bool canTrigger = false;
    public bool isClicked = false;
    public float lastClick = 0f;
    public bool isBeingMoved = false;
    public bool doubleClickTrigger = false;
    private float maxX;
    private float maxY;
    private float minX;
    private float minY;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform parent = this.gameObject.transform.parent;
        minX = parent.position.x - parent.localScale.x/2;
        minY = parent.position.y - parent.localScale.y/2;
        maxX = parent.position.x + parent.localScale.x/2;
        maxY = parent.position.y + parent.localScale.y/2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Translate(float x, float y)
    {
        isBeingMoved = true;
        
        /*** keep object within bounds of table ***/
        Transform transform = this.gameObject.transform;
        if (transform.position.x - transform.parent.localScale.x * transform.localScale.x / 2 + x < minX)
        { x = .1f; }
        else if (transform.position.x + transform.parent.localScale.x * transform.localScale.x / 2 + x > maxX)
        { x = -.1f; }
        if (transform.position.y - transform.parent.localScale.y * transform.localScale.y / 2 + x < minY)
        { y = .1f; }
        else if (transform.position.y + transform.parent.localScale.y * transform.localScale.y / 2 + x > maxY)
        { y = -.1f; }
        
        this.gameObject.transform.Translate(x, y, 0);
            
        if (obj)
        {
            obj.GetComponent<Linked>().isBeingMoved = false;
            obj.transform.Translate(-x, -y, 0);
		
            /* search for bumped object */
			/*
            GameObject highlight1 = obj.transform.Find("highlight").gameObject;
            if (highlight1 != null) {
                if (highlight1.GetComponent<bump>() != null && highlight1.GetComponent<bump>().bumpedObj != null) {
                    GameObject bumpedObj1 = highlight1.GetComponent<bump>().bumpedObj; 
                    bumpedObj1.transform.Translate(-x * 1.5f, -y * 1.5f, 0);
                }	
            }
			*/
        }

		/* search for bumped object */
		/*
		GameObject highlight = this.transform.Find("highlight").gameObject;
		if (highlight != null) {
			if (highlight.GetComponent<bump>() != null && highlight.GetComponent<bump>().bumpedObj != null) {
				GameObject bumpedObj = highlight.GetComponent<bump>().bumpedObj; 
				bumpedObj.transform.Translate(x * 1.5f, y * 1.5f, 0);
			}	
		}
		*/
    }

    public void Stop()
    {
        isBeingMoved = false;
        this.gameObject.GetComponent<FlickDeceleration>().velocity = new Vector2(0,0);
    }

    public void TriggerDoubleClick()
    {
        doubleClickTrigger = !doubleClickTrigger;
    }
}
