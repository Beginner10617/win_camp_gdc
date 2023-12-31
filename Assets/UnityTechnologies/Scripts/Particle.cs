using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            ParticleSystem.Play();
    }
}