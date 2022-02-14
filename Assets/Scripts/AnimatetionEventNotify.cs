using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatetionEventNotify : MonoBehaviour
{
    public bool OnPreparation = false;
    public bool OnContact = false;
    public bool OnRecover = false;
    public bool OnAttacking = false;
    public BoxCollider2D AttackCollision;

    public void Enter_Preparationstep()
    {
        OnPreparation = true;
        OnContact = false;
        OnRecover = false;
        AttackCollision.enabled = false;
        print("1" + AttackCollision.enabled);
}

    public void Enter_Contactstep()
    {
        OnPreparation = false;
        OnContact = true;
        OnRecover = false;
        AttackCollision.enabled = true;
        print("2" + AttackCollision.enabled);
    }

    public void RecoveryStepstep()
    {
        OnPreparation = false;
        OnContact = false;
        OnRecover = true;
        OnAttacking = false;
        AttackCollision.enabled = false;
        print("3" + AttackCollision.enabled);
    }
    
    public void EndStep()
    {
        OnPreparation = false;
        OnContact = false;
        OnRecover = false;
        OnAttacking = false;
        AttackCollision.enabled = false;
        print("4");
    }
}
