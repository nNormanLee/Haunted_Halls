using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProximityScare : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.Equals(player)){
            animator.SetTrigger("scare");
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            animator.SetTrigger("idle");
            audioSource.Stop();
        }
    }
}
