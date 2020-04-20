using System.Collections;
using System.Collections.Generic;
using TankBehaviour;
using UnityEngine;

public class AIStateBase
{
    protected EnemyTankCtrl _owner;

    public AIStateBase(EnemyTankCtrl owner)
    {
        _owner = owner;
    }

    public virtual void OnStateEnter()
    {
        //  Debug.Log("StateEnter:" + this);
    }

    public virtual void OnStateStay()
    {
        //   Debug.Log("StateStay:" + this);
    }

    public virtual void OnStateExit()
    {
        //   Debug.Log("StateExit:" + this);
    }

    public Vector3 GetMoveToPos()
    {
        return _owner.TargetTran.position +
               Random.Range(-3000, 3000) * new Vector3(1, 0, 1);
    }

    public float GetDistToTarget()
    {
        return Vector3.SqrMagnitude(_owner.transform.position - _owner.TargetTran.position);
    }
}

public class AIStateMove : AIStateBase
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _owner.Agent.speed = 10;
    }

    public override void OnStateStay()
    {
        base.OnStateStay();
        if (GetDistToTarget() < _owner.AttributeCtrl._atkDist)
        {
            _owner.AttributeCtrl._isMoving = false;
            _owner.Agent.SetDestination(_owner.transform.position);
            _owner.FsmCtrl.ChangeState(EnemyAIStateType.AISTATE_ATK);
        }
        else
        {
            if (!_owner.AttributeCtrl._isMoving)
            {
                _owner.AttributeCtrl._isMoving = true;
            }
            else
            {
                _owner.Agent.SetDestination(GetMoveToPos());
            }
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public AIStateMove(EnemyTankCtrl owner) : base(owner)
    {
    }
}

public class AIStateAttack : AIStateBase
{
    public override void OnStateEnter()
    {
    }

    public override void OnStateStay()
    {
        if (GetDistToTarget() < _owner.AttributeCtrl._atkDist)
        {
          _owner.Turret.LookAt(_owner.TargetTran);
        }
        else
        {
            _owner.FsmCtrl.ChangeState(EnemyAIStateType.AISTATE_MOVE);
        }

        if (Time.time > _owner.AttributeCtrl._nextAtkTime)
            TryAtkToTarget();
    }

    public void AttackAINormalFire()
    {
        GameObject bullet = _owner.SpawnBullet();
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _owner.AttributeCtrl._bulletSpeed;
    }

    // IEnumerator AttackAIContinueFire()
    // {
    //     for (int i = 0; i < maxAIBulletCount; i++)
    //     {
    //         GameObject bullet = Instantiate(bulletPfb, bulletSpawnTran.position, bulletSpawnTran.transform.rotation);
    //         bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletMoveSpeed;
    //         yield return 1f;
    //     }
    //     _nextAtkTime = Time.time + _atkDelay;
    // }
    private void TryAtkToTarget()
    {
        int randomVal = UnityEngine.Random.Range(0, 2);
        switch (randomVal)
        {
            case 0:
                AttackAINormalFire();
                _owner.AttributeCtrl._nextAtkTime = Time.time + _owner.AttributeCtrl._atkDelay;
                break;
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public AIStateAttack(EnemyTankCtrl owner) : base(owner)
    {
    }
}

public class AIStateDead : AIStateBase
{
    public AIStateDead(EnemyTankCtrl owner) : base(owner)
    {
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _owner.Rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}