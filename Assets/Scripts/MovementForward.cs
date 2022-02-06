using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementForward : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _normal;
    private Vector3 movement;

    [SerializeField] private GameObject _object;
    private GameOver gameOver;


    private void OnCollisionEnter(Collision collision)
    {
            _normal = collision.contacts[0].normal;
    }
    private void Start()
    {
        movement.z = -1;
        gameOver = _object.GetComponent<GameOver>();
    }
    private void FixedUpdate()
    {
        Vector3 surfaceDirection = movement - Vector3.Dot(movement, _normal) * _normal;
        Vector3 offset = surfaceDirection * (_speed * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + offset);
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        if (gameOver.IsDead)
        {
            _speed = 0;
        }
    }
}
