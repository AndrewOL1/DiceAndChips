using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceParticle : MonoBehaviour
{
    ParticleSystem part;
    public bool ghost;
    private void Start()
    {
        if (!ghost)
        {
            part = GetComponent<ParticleSystem>();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag!="Felt" && !collision.gameObject.GetComponent<ghostObj>().ghost) {
            part.Play();
        }
    }
}
