using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    [SerializeField]
    private AudioSource[] _effortSounds;
    [SerializeField]
    private AudioSource _blockSound;
    [SerializeField]
    private Image _healthBar;
    private float _timeToAttack = 0f;
    private Animator _anim;
    private bool _hasHit;
    public static Action CharacterDead;


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
            int indexOfSound = UnityEngine.Random.Range( 0, _effortSounds.Length );
            _timeToAttack = Time.time + _attackCooldown;
            _anim.SetTrigger( "attackTrigger" );
            _effortSounds[indexOfSound].Play();
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
        StartCoroutine( TakeDamageWithDelay() );
    }

    public void PlayerDeath()
    {
        _healthBar.fillAmount = 0;
        _anim.SetBool( "isDying", true );
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.7f;
        transform.position = newPosition;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        Destroy( playerMovement );
        CharacterDead?.Invoke();
        Destroy( this );
    }

    private IEnumerator TakeDamageWithDelay()
    {
        yield return new WaitForSeconds( 0.75f ); 

        if (IsBlocking())
        {
            _health = _health - _damageTaken / 5;
            _blockSound.Play();
        }
        else
        {
            _health = _health - _damageTaken;
        }

        if(_health<= 0)
        {
            PlayerDeath();
        }
        StartCoroutine( UpdateHealthBar( _health / 100f ) );
    }

    private IEnumerator UpdateHealthBar( float targetFillAmount )
    {
        float currentFillAmount = _healthBar.fillAmount;
        float elapsedTime = 0f;
        float duration = 0.5f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _healthBar.fillAmount = Mathf.Lerp( currentFillAmount, targetFillAmount, elapsedTime / duration );
            yield return null;
        }
        _healthBar.fillAmount = targetFillAmount;
    }
}
