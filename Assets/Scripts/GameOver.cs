using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Animator _animator;
    public bool IsDead;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            IsDead = true;
            _animator.SetTrigger("Death");
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Start");
    }
}
