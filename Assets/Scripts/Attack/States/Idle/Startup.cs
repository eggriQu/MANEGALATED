using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : BaseStateATT
{
    public Startup(AttackSM stateMachine, MovementSM stateMachineMV) : base("Startup", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
