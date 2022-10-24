using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasury : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Coin Cart")) return;
        Coins.coins++;
        Destroy(collision.gameObject);
    }
}
