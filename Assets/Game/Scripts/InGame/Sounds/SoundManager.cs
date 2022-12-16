using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SoundManager : SingletonBind<SoundManager>
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    public  AudioClip ClickSound, BackgroundSound;
    public AudioSource audioSrc;
    private bool muted = false;

    private void Start()
    {
        BGPlaySounds();
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }
    public void OnButtonPress()
    {
        muted = !muted;
        AudioListener.pause = muted;
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon()
    {
        soundOffIcon.enabled = muted;
        soundOnIcon.enabled = !muted;
    }
    public  void ClickPlaySounds()
    {
        audioSrc.PlayOneShot(ClickSound);
    }
    public  void BGPlaySounds()
    {
        audioSrc.loop = true;
        audioSrc.PlayOneShot(BackgroundSound);
    }
}
