using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceParticle : MonoBehaviour
{
    ParticleSystem part;
    private void Start()
    {
        part=GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag!="Felt") {
            part.Play();
        }
    }
}
