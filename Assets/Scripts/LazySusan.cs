using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazySusan : MonoBehaviour
{

    List <GameObject> currentCollisions = new List <GameObject> ();
    public GameObject table;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject origObj = other.gameObject;
        if (origObj.GetComponent<Linked>() != null)
        {
            currentCollisions.Add(origObj);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GameObject origObj = other.gameObject;
        if (origObj.GetComponent<Linked>() != null)
        {
            currentCollisions.Remove(origObj);
            //origObj.transform.parent = table.gameObject.transform;
        }
    }

    public void Rotate(Vector2 mousePos, Vector2 mouseStartPos)
    {
        Vector2 susanPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        float angle = Vector2.SignedAngle(mouseStartPos - susanPos, mousePos - susanPos);

        Dictionary<GameObject, Vector3> scales = new Dictionary<GameObject, Vector3>();
        foreach (GameObject obj in currentCollisions)
        {
            scales.Add(obj, obj.transform.localScale);
            obj.transform.parent = this.gameObject.transform;
        }
        this.transform.Rotate(0, 0, angle);
        foreach (GameObject obj in currentCollisions)
        {
            obj.transform.parent = table.transform;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.localScale = scales[obj];
        }
        Debug.Log("Lazy Susan Rotate");
    }
}
