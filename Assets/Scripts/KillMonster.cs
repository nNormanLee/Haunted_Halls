using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMonster : MonoBehaviour
{
    public GameObject monster;
    
    //public GameObject quad;

    public Animator monsterAnimator;

    public AudioSource keyComplete;

    public ParticleSystem keyParticles;


    
    // Start is called before the first frame update
    void Start()
    {
       // keyComplete = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(monster))
        {
            //quad.SetActive(true);
            monsterAnimator.SetTrigger("Death");
            keyComplete.Play();
            keyParticles.Stop();
        }
    }
}
