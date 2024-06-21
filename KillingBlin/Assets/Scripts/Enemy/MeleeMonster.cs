using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Defines.FSMDefines;

public class MeleeMonster : MonsterBase
{

    protected override void Init()
    {
        base.Init();

        fsmManager.ChangeState(MonsterFSMState.Idle);
    }
}
