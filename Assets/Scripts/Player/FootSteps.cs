using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footstepClips;
    private AudioSource audioSource;
    private Rigidbody _rb;
    public float footstepThreshold;
    public float footstepRate;
    private float footStepTime;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Mathf.Abs(_rb.velocity.y) < 0.1f)
        {
            if(_rb.velocity.magnitude > footstepThreshold)
            {
                if(Time.time - footStepTime > footstepRate)
                {
                    footStepTime = Time.time;
                    audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
                }
            }
        }
    }
}
