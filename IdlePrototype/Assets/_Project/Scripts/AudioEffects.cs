using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour
{
    [SerializeField] AudioClip a_click;
    [SerializeField] AudioClip a_clickUpgr;
    [SerializeField] AudioClip a_menuActive;
    [SerializeField] AudioClip a_resourseUp;
    [SerializeField] AudioClip a_shoot1;
    [SerializeField] AudioClip a_expl;
    AudioSource _audioSource;
    //FindAnyObjectByType<AudioEffects>().AudioMenuActive();
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    //public void AudioAgaFamale()
    //{
    //    int ind = Random.Range(0, a_agaF.Length);
    //    _audioSource.PlayOneShot(a_agaF[ind]);
    //}

    public void AudioMenuActive()
    {
        _audioSource.PlayOneShot(a_menuActive);
    }

    public void AudioClick()
    {
        _audioSource.PlayOneShot(a_click);
    }
    public void AudioClickUpgr()
    {
        _audioSource.PlayOneShot(a_clickUpgr);
    }
    public void AudioResUp()
    {
        _audioSource.PlayOneShot(a_resourseUp);
    }
    public void AudioShoot1()
    {
        _audioSource.PlayOneShot(a_shoot1);
    }
    public void AudioExpl()
    {
        _audioSource.PlayOneShot(a_expl);
    }
}
