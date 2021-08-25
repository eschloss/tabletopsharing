using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ClickManager : MonoBehaviour
{
    private GameObject clickedObj = null; 
    private Vector2 firstMousePos = new Vector2(0,0);
    private Vector2 lastMousePos = new Vector2(0,0);
    private bool flickOn = false;
    private const float DOUBLE_CLICK_DELAY = .5f;
    
    // Start is called before the first frame update
    void Start()
    {
        flickOn = Convert.ToBoolean(PlayerPrefs.GetInt("Toggle01"));
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
                if (hit.collider != null) {
                    /*** decals ***/
                    if (hit.collider.gameObject.GetComponent<Linked>() != null)
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
                    /*** Lazy Susan ***/
                    } else if (hit.collider.gameObject.GetComponent<LazySusan>() != null)
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        clickedObj = hit.collider.gameObject;
                    }
                }

                firstMousePos = mousePos2D;
                lastMousePos = mousePos2D;
            }
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Vector2 totalMousePosDiff = mousePos2D - firstMousePos;
            Vector2 mousePosDiff = mousePos2D - lastMousePos;
            
            if (Input.GetMouseButton(0))
            /*** Click and Drag Continue ***/
            {
                /*** decals ***/
                if (clickedObj.GetComponent<Linked>() != null)
                {
                    clickedObj.GetComponent<Linked>().Translate(totalMousePosDiff.x, totalMousePosDiff.y);
                    firstMousePos = mousePos2D;
                /*** Lazy Susan ***/
                } else if (clickedObj.GetComponent<LazySusan>() != null)
                {
                    clickedObj.GetComponent<LazySusan>().Rotate(mousePos2D, lastMousePos);
                }
            }
            else
            {
                /*** Flick ***/
                if (flickOn && clickedObj.GetComponent<FlickDeceleration>() != null)
                {
                    Vector2 flickVelocity = mousePosDiff / Time.deltaTime;
                    if (flickVelocity.x != 0 | flickVelocity.y != 0)
                    {
                        clickedObj.GetComponent<FlickDeceleration>().velocity = flickVelocity;
                    }
                }

                if (clickedObj.GetComponent<Linked>() != null)
                {
                    clickedObj.GetComponent<Linked>().isClicked = false;
                }
                clickedObj = null;
            }
            lastMousePos = mousePos2D;
        }
            
    }
}
