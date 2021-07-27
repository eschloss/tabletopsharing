using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickDeceleration : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0,0);
    public float maxVelocity = 30f;
    public float acceleration = -40f;

    // Update is called once per frame
    void Update()
    {
        if (!this.gameObject.GetComponent<Linked>().isClicked) 
        {
            if (velocity.x != 0 || velocity.y != 0)
            {
                velocity.x += acceleration * Time.deltaTime;
                velocity.y += acceleration * Time.deltaTime;
                if (velocity.x < 0) velocity.x = 0;
                if (velocity.y < 0) velocity.y = 0;
                if (velocity.x > maxVelocity) velocity.x = maxVelocity;
                if (velocity.y > maxVelocity) velocity.y = maxVelocity;

                this.gameObject.GetComponent<Linked>()
                    .Translate(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime);
            }
            else
            {
                this.gameObject.GetComponent<Linked>().Stop();
            }
        }
        
    }
}
