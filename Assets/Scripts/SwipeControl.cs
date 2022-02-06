using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _newPosition;

    public static SwipeControl instance;
    private Vector2 _startTouch;
    private bool _touchMoved;
    private Vector2 _swipeDelta;

    private bool _isGrounded;
    [SerializeField] private float _jumpforce;

    [SerializeField] private BoxCollider _dogSize;
    private Vector3 _startSize;
    private Vector3 _startCenter;

    private Animator _animator;

    Vector2 TouchPosition()
    {
        return (Vector2)Input.mousePosition;
    }
    bool TouchBegan()
    {
        return Input.GetMouseButtonDown(0);
    }
    bool TouchEnded()
    {
        return Input.GetMouseButtonUp(0);
    }
    bool GetTouch()
    {
        return Input.GetMouseButton(0);
    }

    private void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (TouchBegan())
        {
            _startTouch = TouchPosition();
            _touchMoved = true;
        }
        else if (TouchEnded() && _touchMoved == true)
        {
            _touchMoved = false;
        }

        _swipeDelta = Vector2.zero;
        if (_touchMoved && GetTouch())
        {
            _swipeDelta = TouchPosition() - _startTouch;
        }

        if (_swipeDelta.magnitude > 30)
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
            {
                if (_swipeDelta.x > 0)
                {
                    _newPosition = new Vector3(_rigidbody.position.x - 2, _rigidbody.position.y, _rigidbody.position.z);
                }
                else
                {
                    _newPosition = new Vector3(_rigidbody.position.x + 2, _rigidbody.position.y, _rigidbody.position.z);
                }
                _rigidbody.MovePosition(_newPosition);
                Reset();

            }
            else
            {
                if (_swipeDelta.y > 0 && _isGrounded)
                {
                    _isGrounded = false;
                    _rigidbody.AddForce(new Vector3(0, _jumpforce, 0));

                    _animator.SetTrigger("Jump");
                    StartCoroutine(wait());
                    _animator.SetTrigger("Run");
                }
                else
                {
                    _animator.SetTrigger("Crouch");
                    StartCoroutine(crouch());
                    StartCoroutine(wait());
                    _animator.SetTrigger("Run");
                }
                Reset();
            }
        }
    }
    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        _touchMoved = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
    IEnumerator crouch()
    {
        _startSize = _dogSize.size;
        _startCenter = _dogSize.center;
        _dogSize.size = new Vector3(0.15f, 0.233f, 0.55f);
        _rigidbody.mass = 10;
        _dogSize.center = new Vector3(_dogSize.center.x, _dogSize.center.y - 0.1f, _dogSize.center.z);
        yield return new WaitForSeconds(1);
        _dogSize.center = _startCenter;
        _dogSize.size = _startSize;
        _rigidbody.mass = 1;
    }
}
