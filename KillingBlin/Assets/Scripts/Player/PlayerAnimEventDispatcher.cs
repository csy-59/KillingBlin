using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerAnimEventDispatcher : MonoBehaviour
{
    public System.Action OnAttackCallback { get; set; }
    public System.Action OnAttackEndCallback { get; set; }

    public void OnAttackMiddle()
    {
        OnAttackCallback?.Invoke();
    }

    public void OnAttack()
    {
        OnAttackCallback?.Invoke();
    }

    public void OnAttackAnimationEnd()
    {
        OnAttackEndCallback?.Invoke();
    }
}
