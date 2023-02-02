using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    public Sprite LoserSprite;
    public float MaxTime;
    public Image BackgroundImage;
    public Image TimerImage;
    public AudioClip Clip;

    private float _currentTime;
    private AudioSource _backgroundAudio;
    private bool _paused;

    private void Start()
    {
        _currentTime = MaxTime;
        _backgroundAudio = GetComponent<AudioSource>();
        _backgroundAudio.Play();
    }


    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            _currentTime = MaxTime;
        }

        TimerImage.fillAmount = _currentTime / MaxTime;
    }

    public void ManageSprite()
    {
        BackgroundImage.sprite = LoserSprite;
    }

    public void ChangeSound()
    {

        _backgroundAudio.clip = Clip;
        _backgroundAudio.Play();
    }

    public void GamePaused()
    {
        if (_paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        _paused = !_paused;
    }
}


