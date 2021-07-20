using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickManager : MonoBehaviour
{
    private GameObject clickedObj = null;
    private Vector2 lastMousePos = new Vector2(0,0);
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (clickedObj == null)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    clickedObj = hit.collider.gameObject;
                    clickedObj.GetComponent<Linked>().isClicked = true;
                }

                lastMousePos = mousePos2D;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                Vector2 mousePosDiff = mousePos2D - lastMousePos;
                clickedObj.transform.Translate(mousePosDiff.x, mousePosDiff.y, 0);
                
                GameObject linkedObj = clickedObj.GetComponent<Linked>().obj;
                if (linkedObj)
                {
                    linkedObj.transform.Translate(-mousePosDiff.x, -mousePosDiff.y, 0);
                }
                
                lastMousePos = mousePos2D;
            }
            else
            {
                clickedObj.GetComponent<Linked>().isClicked = false;
                clickedObj = null;
            }
            
        }
    }
}
