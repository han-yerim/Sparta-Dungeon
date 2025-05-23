using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    Rigidbody Rigidbody;
    float power = 5.0f;
    public float jumpForce = 300f;
    public AudioClip jumpSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Rigidbody = GetComponent<Rigidbody>();
    }
    void AddForceTest()
    {
        Rigidbody.AddForce(transform.up * power);

        Rigidbody.AddForce(transform.up * power, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound);
                }
            }
        }
    }
}
