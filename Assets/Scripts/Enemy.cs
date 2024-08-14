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
    private float _attackAnimationTime;
    private Animator _anim;
    private float _nextAttackTime;


    private void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.Find( "Player" ).transform;
    }

    private void Update()
    {
        transform.LookAt( _player );
        float distanceToPlayer = Vector3.Distance( transform.position, _player.position );

        if (distanceToPlayer <= _visionRange && distanceToPlayer >= _attackRange)
        {
            RunToEnemy();
        }
        else
        {
            Attack();
        }
    }

    public bool CanAttack()
    {
        return (Time.time >= _nextAttackTime);
    }

    private bool CanRun()
    {
        return (Time.time >= _nextAttackTime);
    }

    private void Attack()
    {
        
        if (CanAttack())
        {
            _nextAttackTime = Time.time + _attackAnimationTime;
            _anim.SetBool( "isRunning", false );
            _anim.SetTrigger( "attackTrigger" );
        }
    }

    private void RunToEnemy()
    {
        if (CanRun())
        {
            _anim.SetBool( "isRunning", true );
            transform.position += transform.forward * _runSpeed * Time.deltaTime;
        }
    }
}
