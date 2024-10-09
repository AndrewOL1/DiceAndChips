using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour

   
{
    public float speed = 1.0f; // Speed of scaling
    private bool scalingDown = true;
    public float LowestY = 0.23f;
    public float HighestY = 0.5f;
    void Update()
    {
        // Determine the scaling factor based on the current state
        float scaleChange = scalingDown ? -speed * Time.deltaTime : speed * Time.deltaTime;

        // Update the Y scale
        transform.localScale += new Vector3(0, scaleChange, 0);

        // Check if we need to switch scaling direction
        if (transform.localScale.y <= LowestY) // Minimum scale
        {
            scalingDown = false; // Switch to scaling up
        }
        else if (transform.localScale.y >= HighestY) // Maximum scale
        {
            scalingDown = true; // Switch to scaling down
        }
    }
}

