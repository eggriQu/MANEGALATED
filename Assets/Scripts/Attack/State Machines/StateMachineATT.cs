using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineATT : MonoBehaviour
{
    public BaseStateATT previousState;
    public BaseStateATT currentState;

    void Start()
    {     
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

    public void ChangeToStartup(BaseStateATT newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public IEnumerator ChangeState(BaseStateATT newState, float startup, float active, float recovery)
    {
        yield return new WaitForSeconds(recovery); // Leave at 0 if going from NotAttacking
        currentState.Exit();
        yield return new WaitForSeconds(startup);
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
        yield return new WaitForSeconds(active);
    }

    public IEnumerator HitStop(float hitstop)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(hitstop);
        Time.timeScale = 1;
    }

    public IEnumerator RecoveryCollider(float time, BoxCollider2D damageCollider)
    {
        yield return new WaitForSeconds(time);

        Vector2 offset = damageCollider.offset;
        offset.y = -3;

        damageCollider.offset = offset;
    }

    public void Spawn(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        Instantiate(prefab, position, rotation);
    }

    protected virtual BaseStateATT GetInitialState()
    {
        return null;
    }
}
