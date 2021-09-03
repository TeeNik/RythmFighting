using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{

    private bool _isMoveStarted;
    private Vector3 _start;
    private Vector3 _target;
    private float _time;
    private float _currentTime;

    private float _startTime;
    private float _endTime;

    public void StartMove(Vector3 start, Vector3 target, float time)
    {
        _isMoveStarted = true;
        _start = start;
        _target = target;
        _time = time;

        _startTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if(_isMoveStarted)
        {
            float deltaTime = Time.deltaTime;
            _currentTime += deltaTime;
            float percent = Mathf.Clamp(_currentTime / _time, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(_start, _target, percent);
            if(percent == 1.0f)
            {
                _isMoveStarted = false;
                print(_startTime + "  " + Time.realtimeSinceStartup);
            }
        }
    }
}
