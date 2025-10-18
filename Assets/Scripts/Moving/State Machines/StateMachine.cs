using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState previousState2;
    public BaseState previousState;
    public BaseState currentState;

    void Start()
    {
        previousState2 = GetInitialState();
        if (previousState2 != null)
        {
            previousState2.Enter();
        }
        
        previousState = GetInitialState();
        if (previousState != null)
        {
            previousState.Enter();
        }

        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        previousState2 = previousState;
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }

    public IEnumerator ChangeFromAttack(BaseState newState, float recovery)
    {
        currentState.Exit();

        yield return new WaitForSeconds(recovery);
        previousState2 = previousState;
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
