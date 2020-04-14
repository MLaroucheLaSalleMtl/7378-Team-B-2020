using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace TankBehaviour
{
    public class EnemyTankCtrl : MonoBehaviour
    {
        #region Properties

        [HideInInspector] [SerializeField] private Transform _curTargetTran;
        [HideInInspector] [SerializeField] private EnemyTankFsmCtrl _curEnemyTankFsmCtrl = null;
        [HideInInspector] [SerializeField] private EnemyTankAttributeCtrl _attributeCtrl = null;
        [HideInInspector] [SerializeField] private NavMeshAgent _agent;
        [HideInInspector] [SerializeField] private Transform _turret;
        [HideInInspector] [SerializeField] private Transform _bulletSpawnPos;
        [HideInInspector] [SerializeField] private GameObject _bullet;
        [HideInInspector] [SerializeField] private Rigidbody _rb;
        public Transform TargetTran => _curTargetTran;
        public NavMeshAgent Agent => _agent;
        public EnemyTankFsmCtrl FsmCtrl => _curEnemyTankFsmCtrl;
        public Transform Turret => _turret;
        public EnemyTankAttributeCtrl AttributeCtrl => _attributeCtrl;
        public Rigidbody Rb => _rb;

        #endregion

        #region Mono APIs

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, 1000);
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _turret = transform.Find("Turret_Objects");
            _bulletSpawnPos = transform.Find("Turret_Objects/Barrel_Base/Bullet_Generator");
        }

        private void Start()
        {
            _curTargetTran = GameObject.FindWithTag(EnumDefine.PlayerTag).transform;
            Init();
        }

        private void Init()
        {
            if (_curEnemyTankFsmCtrl == null)
            {
                _curEnemyTankFsmCtrl = gameObject.AddComponent<EnemyTankFsmCtrl>();
                _curEnemyTankFsmCtrl.Init(this);
            }

            if (_attributeCtrl == null)
            {
                _attributeCtrl = gameObject.AddComponent<EnemyTankAttributeCtrl>();
                _attributeCtrl.Init(this);
            }

            if (_agent == null)
                _agent = GetComponent<NavMeshAgent>();
            _curEnemyTankFsmCtrl.ExecuteAILogic();
        }

        public void OnDead()
        {
            _curEnemyTankFsmCtrl.StopAI();
        }

        public GameObject SpawnBullet()
        {
            return Instantiate(_bullet, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
        }

        #endregion
    }
}