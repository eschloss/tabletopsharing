using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linked : MonoBehaviour
{
    public GameObject obj;
    public bool canTrigger = false;
    public bool isClicked = false;
    public bool isBeingMoved = false;
    private float maxX;
    private float maxY;
    private float minX;
    private float minY;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform parent = this.gameObject.transform.parent;
        minX = parent.localPosition.x;
        minY = parent.localPosition.y;
        maxX = parent.localScale.x + minX;
        maxY = parent.localScale.y + minY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Translate(float x, float y)
    {
        isBeingMoved = true;
        
        /*** keep object within bounds of table ***/
        /* TODO make this work tomorrow
        Transform transform = this.gameObject.transform;
        if (x + transform.localPosition.x + transform.localScale.x > maxX) x = maxX;
        if (y + transform.localPosition.y + transform.localScale.y > maxY) y = maxY;
        if (x + transform.localPosition.x < minX) x = minX;
        if (y + transform.localPosition.y < minY) y = minY;
        */
        
        this.gameObject.transform.Translate(x, y, 0);
            
        if (obj)
        {
            obj.GetComponent<Linked>().isBeingMoved = false;
            obj.transform.Translate(-x, -y, 0);
        }
    }

    public void Stop()
    {
        isBeingMoved = false;
    }
}
