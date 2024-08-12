using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3f;
    private Transform _parent;

    private void Start()
    {
        _parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis( "Mouse X" ) * _mouseSensitivity * Time.deltaTime;
        _parent.Rotate( Vector3.up, mouseX );
    }
}
