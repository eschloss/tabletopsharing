using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Sprite puzzle1;
    public GameObject table;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = new GameObject();
        go.transform.parent = table.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
