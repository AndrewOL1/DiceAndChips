using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public ScoreManager scoreManager;
    [SerializeField]public int score;
    void Start()
    {
        scoreManager=GameObject.Find("Canvas").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chip" && !other.gameObject.GetComponent<ghostObj>().ghost)
        {
            scoreManager.updateScore(score);

        }
    }
}
