using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TouchManager : MonoBehaviour
{
    private List<int> touchIds = new List<int>();
    private Dictionary<int, GameObject> clickedObj = new Dictionary<int, GameObject>();
    private Dictionary<int, Vector2> firstMousePos = new Dictionary<int, Vector2>();
    private Dictionary<int, Vector2> lastMousePos = new Dictionary<int, Vector2>();
    private Dictionary<int, Vector2> prevMousePos = new Dictionary<int, Vector2>();
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
        List<int> activeTouchIds = new List<int>();
        
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            int id = touch.fingerId;
            activeTouchIds.Add(id);
            
            Vector2 mousePos2D = touch.position;
            if (clickedObj[id] == null)
            {
                /*** Click and Drag Onset ***/
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null) {
                    /*** decals ***/
                    if (hit.collider.gameObject.GetComponent<Linked>() != null)
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        clickedObj[id] = hit.collider.gameObject;
                        Linked linked = clickedObj[id].GetComponent<Linked>();
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
                        clickedObj[id] = hit.collider.gameObject;
                    }
                }

                firstMousePos[id] = mousePos2D;
                prevMousePos[id] = mousePos2D;
                lastMousePos[id] = mousePos2D;
            }
            else
            {
                Vector2 totalMousePosDiff = mousePos2D - firstMousePos[id];
                Vector2 mousePosDiff = mousePos2D - lastMousePos[id];
                
                /*** Click and Drag Continue ***/
                /*** decals ***/
                if (clickedObj[id].GetComponent<Linked>() != null)
                {
                    clickedObj[id].GetComponent<Linked>().Translate(totalMousePosDiff.x, totalMousePosDiff.y);
                    firstMousePos[id] = mousePos2D;
                /*** Lazy Susan ***/
                } else if (clickedObj[id].GetComponent<LazySusan>() != null)
                {
                    clickedObj[id].GetComponent<LazySusan>().Rotate(mousePos2D, lastMousePos[id]);
                }

                prevMousePos[id] = lastMousePos[id];
                lastMousePos[id] = mousePos2D;
            }
        }

        foreach (int id in touchIds)
        {
            if (!activeTouchIds.Contains(id))
            {
                /*** Flick ***/
                if (flickOn && clickedObj[id].GetComponent<FlickDeceleration>() != null)
                {
                    Vector2 mousePosDiff = lastMousePos[id] - prevMousePos[id];
                    Vector2 flickVelocity = mousePosDiff / Time.deltaTime;
                    if (flickVelocity.x != 0 | flickVelocity.y != 0)
                    {
                        clickedObj[id].GetComponent<FlickDeceleration>().velocity = flickVelocity;
                    }
                }

                if (clickedObj[id].GetComponent<Linked>() != null)
                {
                    clickedObj[id].GetComponent<Linked>().isClicked = false;
                }
                clickedObj.Remove(id);
                firstMousePos.Remove(id);
                lastMousePos.Remove(id);
                prevMousePos.Remove(id);
            }
        }
        touchIds = activeTouchIds;
    }
}
