using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickObject : MonoBehaviour
{
    [SerializeField] Animator anim;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // [SerializeField] List<String> aniList;

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (audioSource.clip == null && audioClip != null)
        {
            audioSource.clip = audioClip;
        }
    }

    void OnMouseUp()
    {
        // Debug.Log("MouseDown");
        if (!anim.GetBool("isTouch"))
        {
            PlayAudio();
        }

        anim.SetBool("isTouch", true);
    }

    public void SetIsTouchFalse()
    {
        anim.SetBool("isTouch", false);
    }

    public void PlayAudio()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.Play();
        }
    }
}
