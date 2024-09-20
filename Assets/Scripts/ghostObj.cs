using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ghostObj : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ghost=false;
    private bool hadRigidbody=false;
    void Start()
    {
        if(GetComponent<Rigidbody>() != null&&ghost)
        {
            Destroy(GetComponent<Rigidbody>());
            hadRigidbody = true;
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (ghost && hadRigidbody)
        {
            TrajectorySim.Instance.Hit();
        }
    }
}
