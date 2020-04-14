using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBehaviour
{
    public class EnemyTankAttributeCtrl : MonoBehaviour
    {
        [SerializeField] private Slider _hpBar = null;
        [SerializeField] private int _maxHp = 100;
        [SerializeField] private int _curHp = 100;
        private EnemyTankCtrl _owner;
        [HideInInspector] public float _atkDist = 1000;
        [HideInInspector] public bool _isMoving = false;
        [HideInInspector] public float _rotateSpeed = 1;
        [HideInInspector] public float _atkDelay = 5f;
        [HideInInspector] public float _nextAtkTime = 5f;
        [HideInInspector] public float _bulletSpeed = 100f;

        public int CurHp
        {
            get => _curHp;
            set
            {
                if (_curHp <= 0)
                    OnDead();
                else
                    _curHp = value;
                UpdateUIView();
            }
        }

        private void Awake()
        {
            _hpBar = GetComponentInChildren<Slider>();
        }

        private void Start()
        {
        
        }

        public void Init(EnemyTankCtrl owner)
        {
            _owner = owner;
            CurHp = _maxHp;
        }

        public void OnTakeDamage(int dmg)
        {
            CurHp -= dmg;
        }

        public void UpdateUIView()
        {
            _hpBar.value = _curHp;
        }

        public void OnDead()
        {
            _owner.OnDead();
        }
    }
}