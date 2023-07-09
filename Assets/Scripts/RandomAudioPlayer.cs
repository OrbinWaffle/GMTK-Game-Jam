using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource aud = GetComponent<AudioSource>();
        aud.clip = clips[Random.Range(0, clips.Length)];
        aud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
