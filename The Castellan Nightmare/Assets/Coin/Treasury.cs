using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Treasury : MonoBehaviour
{
    [SerializeField] private CoinCart cart;
    [SerializeField] private Transform cartSpawnLocation;

    private void OnEnable()
    {
        Upgrade.requestingCoinCarts += SendCarts;
    }

    private void OnDisable()
    {
        Upgrade.requestingCoinCarts -= SendCarts;
    }

    private void SendCarts((Transform target, int amount) request)
    {
        StartCoroutine(SpawnCarts(request));
    }

    private IEnumerator SpawnCarts((Transform target, int amount) request)
    {
        int amountToSpawn = request.amount;
        while(amountToSpawn > 0)
        {
            Coins.coins--;
            CoinCart spawnedCart = Instantiate(cart.gameObject, cartSpawnLocation.position, Quaternion.identity, transform).GetComponent<CoinCart>();
            spawnedCart.Spawned(this, request.target, true);
            amountToSpawn--;
            yield return new WaitForSeconds(.25f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Coin Cart")) return;
        if (collision.GetComponent<CoinCart>().FromTreasury) return;
        Coins.coins++;
        Destroy(collision.gameObject);
    }
}
