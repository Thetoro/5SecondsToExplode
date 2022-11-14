using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeToExplode : MonoBehaviour
{
    private float _timeToExplode = 5f;
    public TMP_Text timeText;
    private bool _isLaunched;

    private void OnEnable() 
    {
        BombMovement.onLaunch += isLaunch;
    }

    private void OnDisable() 
    {
        BombMovement.onLaunch -= isLaunch;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isLaunched)
            _timeToExplode -= Time.deltaTime;
        
        if(_timeToExplode < 0)
        {
            _timeToExplode = 0;
            _isLaunched = false;
        }
        timeText.text = "Explosion in " + _timeToExplode.ToString("n2");
        //Debug.Log(_timeToExplode);
    }

    public void isLaunch()
    {
        _isLaunched = true;
    }
}
