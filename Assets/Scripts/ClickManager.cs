using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ClickManager : MonoBehaviour
{
    private GameObject clickedObj = null;
    private Vector2 lastMousePos = new Vector2(0,0);
    private bool flickOn = false;
    private Vector2 flickVelocity = new Vector2(0,0);
    private const float DOUBLE_CLICK_DELAY = .5f;
    
    // Start is called before the first frame update
    void Start()
    {
        flickOn = Convert.ToBoolean(PlayerPrefs.GetInt("Toggle01"));
        Debug.Log(flickOn);
        //Debug.Log("Boundaries " + boundariesOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (clickedObj == null)
        {
            /*** Click and Drag Onset ***/
            if (Input.GetMouseButton(0)) 
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.GetComponent<Linked>() != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    clickedObj = hit.collider.gameObject;
                    Linked linked = clickedObj.GetComponent<Linked>();
                    linked.isClicked = true;

                    /*** Double Click Toggle ***/
                    if (Time.time - linked.lastClick < DOUBLE_CLICK_DELAY)
                    {
                        linked.TriggerDoubleClick();
                        linked.lastClick = 0f;
                    }
                    else
                    {
                        linked.lastClick = Time.time;
                    }
                        
                }

                lastMousePos = mousePos2D;
            }
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Vector2 mousePosDiff = mousePos2D - lastMousePos;
            
            if (Input.GetMouseButton(0))
            /*** Click and Drag Continue ***/
            {
                clickedObj.GetComponent<Linked>().Translate(mousePosDiff.x, mousePosDiff.y);
                lastMousePos = mousePos2D;
            }
            else
            {
                /*** Flick ***/
                if (flickOn)
                {
                    flickVelocity = mousePosDiff / Time.deltaTime;
                    if (flickVelocity.x != 0 | flickVelocity.y != 0)
                    {
                        clickedObj.GetComponent<FlickDeceleration>().velocity = flickVelocity;
                    }
                }
                
                clickedObj.GetComponent<Linked>().isClicked = false;
                clickedObj = null;
            }
        }
    }
}
