using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 물건의 이동상태에 따라 사운드를 재생하기 위한 스크립트.
/// 
/// 2021.08.25. KJ
/// 
/// </summary>

public class CollisionDetector_MovableObject : MonoBehaviour
{
    AudioSource sfxSource;
    public AudioClip sfxClip;

    [Range(-3, 3)]
    public float pitch = 1;

    [Range(0, 1)]
    public float volumn = 1;

    private bool InContact = false;

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        sfxSource.pitch = pitch;
    }

    private void Update()
    {
        if (InContact == true && sfxSource.isPlaying == false)
        {
            if (Input.GetKey(KeyCode.W) == true|| Input.GetKey(KeyCode.S) == true)
            {
                sfxSource.PlayOneShot(sfxClip);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InContact = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InContact = false;
        }
    }
}
