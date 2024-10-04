using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DiceParticle : MonoBehaviour
{
    ParticleSystem part;
    public bool ghost;
    private AudioSource audioSource;
    [SerializeField] private AudioClip chipHit;
    [SerializeField] private AudioClip feltHit;
    [SerializeField] private AudioClip tableHit;
    private void Start()
    {
        if (!ghost)
        {
            part = GetComponent<ParticleSystem>();
            audioSource=GetComponent<AudioSource>();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Chip" && !collision.gameObject.GetComponent<ghostObj>().ghost)
        {
            part.Play();
            audioSource.PlayOneShot(chipHit);
        }
        if (collision.gameObject.tag == "Felt" && !collision.gameObject.GetComponent<ghostObj>().ghost)
        {
            audioSource.PlayOneShot(feltHit);
        }
        if (collision.gameObject.tag == "Table" && !collision.gameObject.GetComponent<ghostObj>().ghost)
        {
            audioSource.PlayOneShot(tableHit);
        }
    }
}
