using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _damageTaken;
    [SerializeField]
    private float _maxDistance = 0.5f;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private float _attackCooldown;
    private float _timeToAttack = 0f;
    private Animator _anim;
    private bool _hasHit;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAttacking())
        {
            PerformRaycast();
        }
        if (Input.GetKeyDown( KeyCode.Mouse0 ))
            Attack();
        _anim.SetBool( "isBlocking", IsBlocking() );
    }

    private void Attack()
    {
        if (_timeToAttack < Time.time)
        {
            _timeToAttack = Time.time + _attackCooldown;
            _anim.SetTrigger( "attackTrigger" );
        }
    }

    private bool IsAttacking()
    {
        if (_timeToAttack > Time.time)
            return true;
        else
        {
            _hasHit = false;
            return false;
        }

    }

    private bool IsBlocking()
    {
        if (Input.GetKey( KeyCode.Mouse1 ))
            return true;
        else
            return false;
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast( _weapon.transform.position, _weapon.transform.up, out hit, _maxDistance ))
        {
            if (hit.collider.CompareTag( "Enemy" ) && !_hasHit)
            {
                EnemyCombat enemy = hit.collider.GetComponent<EnemyCombat>();
                enemy.TakeDamage();
                _hasHit = true;
            }
        }
    }

    public void TakeDamage()
    {
        if(IsBlocking())
            _health = _health - _damageTaken/5;
        else
            _health = _health - _damageTaken;
        Debug.Log( _health );
    }
}