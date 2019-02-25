using System;
using UnityEngine;

public class ColumnController : QuestObject
{
    private float _activationDistance = 5f;
    private float _angle;
    private Vector3 _previousPosition;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= _activationDistance)
        {
            UpdateAngle();
        }
        else
        {
            if (Math.Abs(_angle) > 0)
            {
                StopActivation();
            }
        }

        if (Mathf.Abs(_angle) >= 360)
        {
            ActivateObject();
        }
    }

    private void UpdateAngle()
    {
        if (_previousPosition == Vector3.zero)
        {
            SavePosition();
        }
        else if (_previousPosition != Player.transform.position)
        {
            _angle += Vector3.SignedAngle(_previousPosition - transform.position, Player.transform.position - transform.position, Vector3.up);
            SavePosition();
        }
    }

    private void SavePosition()
    {
        _previousPosition = Player.transform.position;
    }

    private void StopActivation()
    {
        _angle = 0f;
        _previousPosition = Vector3.zero;
    }
}
