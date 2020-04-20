using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBehaviour
{
    public class EnemyTankAttributeCtrl : MonoBehaviour
    {
        [SerializeField] private Slider _hpBar = null;
        public float _maxHp = 2500;
        public float _curHp;
        private EnemyTankCtrl _owner;
        public float _atkDist = 15000;
        [HideInInspector] public bool _isMoving = false;
        [HideInInspector] public float _rotateSpeed = 1;
        [HideInInspector] public float _atkDelay = 5f;
        [HideInInspector] public float _nextAtkTime = 5f;
        [HideInInspector] public float _bulletSpeed = 100f;

        //public int CurHp
        //{
        //    get => _curHp;
        //    set
        //    {
        //        if (_curHp <= 0)
        //            OnDead();
        //        else
        //            _curHp = value;
        //        UpdateUIView();
        //    }
        //}

        private void Awake()
        {
            _hpBar = GetComponentInChildren<Slider>();
            _curHp = 2500;
        }

        private void Start()
        {
        
        }

        private void Update()
        {
            if (_curHp <= 0) OnDead();
            print("Current " + _curHp);
            UpdateUIView();
        }

        public void Init(EnemyTankCtrl owner)
        {
            _owner = owner;
            _curHp = _maxHp;
        }

        public void OnTakeDamage(int dmg)
        {
            _curHp-= dmg;
            print("Current " + _curHp);
        }

        public void UpdateUIView()
        {
            _hpBar.value = (_curHp/_maxHp) * 100;
        }

        public void OnDead()
        {
            _owner.OnDead();
        }
    }
}