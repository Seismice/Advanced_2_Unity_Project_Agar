using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundEfect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
