using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown( KeyCode.Mouse0 ))
            Attack();
        _anim.SetBool( "isBlocking", IsBlocking() );
            
    }

    private void Attack()
    {
        _anim.SetTrigger( "attackTrigger" );
    }

    private bool  IsBlocking()
    {
        if (Input.GetKey( KeyCode.Mouse1 ))
            return true;
        else
            return false;
    }
}
