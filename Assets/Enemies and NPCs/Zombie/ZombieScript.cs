using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : EnemyScript
{
    public override void Start()
    {
        maxHp = 200;
        base.Start();
        Debug.Log("Start");

        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Assets/MainCharacter/MainCharacter.controller", typeof(RuntimeAnimatorController));
    }
}
