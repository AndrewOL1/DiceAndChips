using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChips : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Dice")
        {
            ScoreManager.scoreCount +=1;
        }
    }

}
