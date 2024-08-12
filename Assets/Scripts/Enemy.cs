using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _visionRange;
    [SerializeField]
    private float _attackAnimationWaitTime;

    private float _lastAttackTime;
    private float _timeToStartRunAgain;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.LookAt( _player );
        float distanceToPlayer = Vector3.Distance( transform.position, _player.position );

        if (distanceToPlayer <= _visionRange && distanceToPlayer >= _attackRange)
        {
            RunToEnemy();
        }
        else if (distanceToPlayer < _attackRange && CanMoveAgain())
        {
            Attack();
        }
    }

    private bool CanMoveAgain()
    {
        return Time.time >= _timeToStartRunAgain;
    }

    private void Attack()
    {
        _lastAttackTime = Time.time;
        _timeToStartRunAgain = _lastAttackTime + _attackAnimationWaitTime;
        _anim.SetBool( "isRunning", false );
        _anim.SetTrigger( "attackTrigger" );
    }

    private void RunToEnemy()
    {
        if (CanMoveAgain())
        {
            _anim.SetBool( "isRunning", true );
            transform.position += transform.forward * _runSpeed * Time.deltaTime;
        }
    }
}
