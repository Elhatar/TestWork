using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    public int coins = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            Destroy(other.gameObject);
            _scoreText.text = coins.ToString();
        }
    }
}
