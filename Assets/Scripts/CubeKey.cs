using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKey : MonoBehaviour
{
    public GameObject otherCube;
    
    public GameObject quad;

    public Animator mortician;

    public AudioSource keyComplete;

    public ParticleSystem keyParticles;


    void Awake()
    {
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.Equals(otherCube))
        {
            quad.SetActive(true);
            mortician.SetTrigger("ThumbsUp");
            keyComplete.Play();
            keyParticles.Stop();
        }
    }
}
