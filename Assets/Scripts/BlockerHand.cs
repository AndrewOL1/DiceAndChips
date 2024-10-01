using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerHand : MonoBehaviour
{
    LineRenderer lineRenderer;
    public int posCount;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer= GameObject.Find("Hand").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lineRenderer != null) 
        {
            if (lineRenderer.positionCount > 0)
            {
                if(lineRenderer.positionCount< posCount){
                    posCount = lineRenderer.positionCount-1;
                }
                transform.position=Vector3.Lerp(transform.position, lineRenderer.GetPosition(posCount),0.01f);
            }
        
        }
    }
}
