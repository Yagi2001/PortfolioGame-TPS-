using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _damageTakenWithHit;
    public void TakeDamage()
    {
        _health -= _damageTakenWithHit;
        if (_health <= 0)
        {
            //Here I will add death anim
            Destroy( gameObject );
        }
    }
}
