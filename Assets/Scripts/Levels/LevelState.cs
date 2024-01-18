using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    protected enum State { Stopped, Started, Succeed }

    private static State _currentState = State.Stopped;

    protected State CurrentState
    {
        get => _currentState; set { _currentState = value; }
    }

    private void Start()
    {
        CurrentState = State.Started;
    }
}
