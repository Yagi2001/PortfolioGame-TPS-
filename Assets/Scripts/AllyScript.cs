using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _waveObjects;
    [SerializeField]
    private float _checkInterval;
    [SerializeField]
    private GameObject _smokeVFX;

    private void Start()
    {
        InvokeRepeating( "CheckForWaveObjects", 0f, _checkInterval );
    }
    private void CheckForWaveObjects()
    {
        int DeadCounter = 0;
        foreach (GameObject waveObject in _waveObjects)
        {
            EnemyCombat enemyCombat = waveObject.GetComponent<EnemyCombat>();
            if (enemyCombat.isDead)
                DeadCounter++;
            else
                DeadCounter = 0;
        }
        if(DeadCounter == 3)
        {
            SavedAction();
        }
    }

    private void SavedAction()
    {
        Instantiate( _smokeVFX, transform.position, Quaternion.identity );
        Destroy( gameObject );
    }


}
