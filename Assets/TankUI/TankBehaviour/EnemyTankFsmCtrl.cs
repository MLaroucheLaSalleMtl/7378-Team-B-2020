using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBehaviour
{
    public class EnemyTankFsmCtrl : MonoBehaviour
    {
        #region Properties

        public EnemyTankCtrl CurFSMOwner;
        public AIStateBase CurState = null;
        public EnemyAIStateType CurStateType = EnemyAIStateType.AISTATE_NULL;
        public Dictionary<EnemyAIStateType, AIStateBase> StateDict;
        private bool isAIStop = false;

        #endregion

        public void Init(EnemyTankCtrl ctrl)
        {
            CurFSMOwner = ctrl;
            RegisterState();
        }

        public void ChangeState(EnemyAIStateType stateType)
        {
            if (CurStateType == stateType)
            {
                return;
            }

            if (CurState != null)
                CurState.OnStateExit();
            if (stateType != EnemyAIStateType.AISTATE_NULL)
                CurState = StateDict[stateType];
            CurStateType = stateType;
            CurState.OnStateEnter();
        }


        public void RegisterState()
        {
            StateDict = new Dictionary<EnemyAIStateType, AIStateBase>();
            StateDict[EnemyAIStateType.AISTATE_MOVE] = new AIStateMove(CurFSMOwner);
            StateDict[EnemyAIStateType.AISTATE_ATK] = new AIStateAttack(CurFSMOwner);
            StateDict[EnemyAIStateType.AISTATE_DEAD] = new AIStateDead(CurFSMOwner);
        }

        public void ExecuteAILogic()
        {
            ChangeState(EnemyAIStateType.AISTATE_MOVE);
        }

        public void StopAI()
        {  
            ChangeState(EnemyAIStateType.AISTATE_DEAD);
            isAIStop = true;
        }

        #region MonoAPIs

        private void Update()
        {
            if (CurStateType != EnemyAIStateType.AISTATE_NULL)
                CurState.OnStateStay();
        }

        private void OnDestroy()
        {
            if (isAIStop) return;
            CurState = null;
        }

        #endregion
    }
}