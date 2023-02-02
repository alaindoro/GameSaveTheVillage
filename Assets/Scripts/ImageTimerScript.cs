using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimerScript : MonoBehaviour
{

    public float MaxTime;
    public bool IsTimerEnd;

    private Image _image;
    private float _currentTime;

    void Start()
    {
        _image = GetComponent<Image>();
        _currentTime = MaxTime;
    }

    void Update()
    {
        IsTimerEnd = false;
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            IsTimerEnd = true;
            _currentTime = MaxTime;
        }

        _image.fillAmount = _currentTime / MaxTime;
    }
}
