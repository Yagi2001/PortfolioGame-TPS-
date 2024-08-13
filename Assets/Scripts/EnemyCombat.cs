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
    private Enemy Enemy;
    private bool _hasHit;

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        PerformRaycast();
    }
    public void TakeDamage()
    {
        _health -= _damageTakenWithHit;
        if (_health <= 0)
        {
            //Here I will add death anim
            Destroy( gameObject );
        }
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast( _weapon.transform.position, _weapon.transform.up, out hit, _maxDistance ))
        {
            if (hit.collider.CompareTag( "Player" ) && !Enemy.CanMoveAgain())
            {
                Debug.Log( "Hit" + hit.collider.name );
                //EnemyCombat enemy = hit.collider.GetComponent<EnemyCombat>();
                //enemy.TakeDamage();
                //_hasHit = true;
            }
        }
    }
}
