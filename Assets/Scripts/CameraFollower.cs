using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform Target;
    private Vector3 _newPosition;

    private void FixedUpdate()
    {
        _newPosition.x = Target.position.x;
        _newPosition.y = Target.position.y + 2.3f;
        _newPosition.z = Target.position.z + 4f;
        transform.position = _newPosition;
    }
}
