using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource _clickButton;
    [SerializeField] AudioSource _createWarrior;
    [SerializeField] AudioSource _createFarmer;
    [SerializeField] AudioSource _collectWheat;
    [SerializeField] AudioSource _attack;
    [SerializeField] AudioSource _ensureWarriors;
    [SerializeField] AudioSource _gameScreenSound;
    [SerializeField] AudioSource _fauilureScreenSound;
    [SerializeField] AudioSource _winnerScreenSound;

    [SerializeField] private bool _isPlaying = true;

    public void SwitchSound()
    {
        _isPlaying = !_isPlaying;
        PlayAudioClickButton();
        PlayBackgroundAudio();
    }

    public void PlayAudioClickButton()
    {
        if (_isPlaying)
            _clickButton.Play();
    }
    public void PlayAudioCreateWarrior()
    {
        if (_isPlaying)
            _createWarrior.Play();
    }
    public void PlayAudioCreateFarmer()
    {
        if (_isPlaying)
            _createFarmer.Play();
    }
    public void PlayAudioCollectWheat()
    {
        if (_isPlaying)
            _collectWheat.Play();
    }
    public void PlayAudioAttack()
    {
        if (_isPlaying)
            _attack.Play();
    }
    public void PlayAudioEnsureWarriors()
    {
        if (_isPlaying)
            _ensureWarriors.Play();
    }

    public void PlayBackgroundAudio()
    {
        if (_isPlaying)
        {
            _gameScreenSound.mute = false;
            _winnerScreenSound.mute = false;
            _fauilureScreenSound.mute = false;
        }
        else
        {
            _gameScreenSound.mute = true;
            _winnerScreenSound.mute = true;
            _fauilureScreenSound.mute = true;
        }
    }
}
