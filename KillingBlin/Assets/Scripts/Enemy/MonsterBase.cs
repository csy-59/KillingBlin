using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.MonsterDefines;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] private Map map;
    [SerializeField] private MonsterFSMManager fsmManager;

    [SerializeField] private byte type;
    public byte Type { get => type; private set => type = value; }


    public virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {
    }
}
