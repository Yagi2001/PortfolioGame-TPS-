using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCombat : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _damageTakenWithHit;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private float _maxDistance = 0.5f;
    [SerializeField]
    private float _waitTimeForNextAttack;
    private Enemy Enemy;
    private float _timeToAttack;
    private Animator _anim;
    public bool isDead;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        PerformRaycast();
    }
    public void TakeDamage()
    {
        StartCoroutine( TakeDamageWithDelay() );
    }

    private IEnumerator TakeDamageWithDelay()
    {
        yield return new WaitForSeconds( 0.15f );

        _health -= _damageTakenWithHit;
        if (_health <= 0 && !isDead)
        {
            isDead = true;
            //Here I will add death anim
            //Destroy( gameObject );
            _anim.SetBool( "isDead", true );
            GameManager.deadEnemyCounter++;
            Destroy( Enemy );
        }
    }

    private bool HasHit()
    {
        if (_timeToAttack+0.5f <= Time.time) //0.5f is used to control the PerformRayCast
            return false;
        else
            return true;
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast( _weapon.transform.position, _weapon.transform.up, out hit, _maxDistance ))
        {
            if (hit.collider.CompareTag( "Player" )&& !Enemy.CanAttack()&&!HasHit())
            {
                _timeToAttack = Time.time + _waitTimeForNextAttack;
                PlayerCombat player = hit.collider.GetComponent<PlayerCombat>();
                if(player!=null)
                    player.TakeDamage();
            }
        }
    }
}
