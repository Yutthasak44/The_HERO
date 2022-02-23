using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualButtonState : MonoBehaviour
{
    public enum State { Up, Down }
    public State _currentState;
    Color mycolor;
    public void SetDownState()
    { 
        _currentState = State.Down;
        print("Button : " + this.name + " is " + _currentState.ToString());  
    }

    public void SetUpState() 
    {
        _currentState = State.Up;
        print("Button : " + this.name + " is " + _currentState.ToString());
    }
}
