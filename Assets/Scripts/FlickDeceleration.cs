using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlickDeceleration : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0,0);
    public float maxVelocity = 30f;
    private const float decelerationIndex = .01f; //make this larger for a slower deceleration
    private const float minVelocity = 1f;
        

    // Update is called once per frame
    void Update()
    {
        if (!this.gameObject.GetComponent<Linked>().isClicked) 
        {
            if (velocity.x != 0 || velocity.y != 0)
            {
                velocity.x *= (float)Math.Pow(decelerationIndex, Time.deltaTime);
                velocity.y *= (float)Math.Pow(decelerationIndex, Time.deltaTime);
                if (velocity.x > 0 && velocity.x < minVelocity ||
                    velocity.x < 0 && velocity.x > -minVelocity) 
                { velocity.x = 0; }
                if (velocity.y > 0 && velocity.y < minVelocity ||
                    velocity.y < 0 && velocity.y > -minVelocity) 
                { velocity.y = 0; }
                
                if (velocity.x > maxVelocity) velocity.x = maxVelocity;
                if (velocity.y > maxVelocity) velocity.y = maxVelocity;
                if (velocity.x < -maxVelocity) velocity.x = -maxVelocity;
                if (velocity.y < -maxVelocity) velocity.y = -maxVelocity;

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
