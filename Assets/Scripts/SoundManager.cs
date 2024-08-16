using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _normalMusic;
    [SerializeField]
    private AudioSource _combatMusic;
    [SerializeField]
    private float _detectionRadius = 10f; 
    [SerializeField]
    private float _checkInterval = 0.5f; // Used for optimization
    [SerializeField]
    private Transform _player; 

    private void Start()
    {
        _normalMusic.Play();
        InvokeRepeating( "CheckForEnemies", 0f, _checkInterval );
    }

    private void CheckForEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere( _player.position, _detectionRadius );
        bool enemyNearby = false;

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag( "Enemy" ))
            {
                enemyNearby = true;
                break;
            }
        }

        if (enemyNearby && !_combatMusic.isPlaying)
        {
            SwitchToCombatMusic();
        }
        else if (!enemyNearby && !_normalMusic.isPlaying)
        {
            SwitchToNormalMusic();
        }
    }

    private void SwitchToCombatMusic()
    {
        _normalMusic.Stop();
        _combatMusic.Play();
    }

    private void SwitchToNormalMusic()
    {
        _combatMusic.Stop();
        _normalMusic.Play();
    }
}
