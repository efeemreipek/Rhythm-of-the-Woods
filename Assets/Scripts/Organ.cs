using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{
    private Health creature;
    private AudioSource audioSource;

    private void Awake()
    {
        creature = GetComponentInChildren<Health>();
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
    }

    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            GameObject.Find("GameController").GetComponent<GameController>().GameOver(true);
        }
        audioSource.volume = creature.GetHealthNormalized();
    }

    public bool GetIsDead() => creature.GetIsDead();
}
