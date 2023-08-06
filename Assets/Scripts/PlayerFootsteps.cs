using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepsArray;

    private AudioSource audioSource;
    private FirstPersonController firstPersonController;

    private bool isSprinting = false;
    private bool isWalking = false;
    private float footstepInterval;
    private float footstepTimer;
    private float walkSpeed = 5f;
    private float runSpeed = 7f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        firstPersonController = GetComponent<FirstPersonController>();

        footstepInterval = 1f / walkSpeed * 2f;
    }

    private void Update()
    {
        isSprinting = firstPersonController.GetIsSprinting();
        isWalking = firstPersonController.GetIsWalking();

        footstepInterval = isSprinting ? 1f / runSpeed * 2f : 1f / walkSpeed * 2f; 

        if(!isWalking && !isSprinting)
        {
            audioSource.Stop();
        }

        if (isWalking || isSprinting)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstepSound();
                footstepTimer = footstepInterval;
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepsArray.Length > 0)
        {
            int index = Random.Range(0, footstepsArray.Length);
            audioSource.clip = footstepsArray[index];
            audioSource.Play();
        }
    }
}
