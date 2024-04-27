using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNEnemyEvent : MonoBehaviour
{
    #region Action
    private Action shoot;
    private Action Run;
    private Action Dead;
    #endregion

    #region Init
    public void Init(Action shoot, Action Run, Action Dead)
    {
        this.shoot = shoot;
        this.Run = Run;
        this.Dead = Dead;
    }
    #endregion

    #region Event SK
    public void ShootEvent()
    {
        shoot();
    }

    public void RunEvent()
    {
        Run();
    }

    public void DeadEvent()
    {
        Dead();
    }
    #endregion
}
