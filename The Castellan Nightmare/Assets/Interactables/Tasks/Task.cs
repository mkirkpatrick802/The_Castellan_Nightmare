using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Task : Interactable
{
    [Header("Task Settings")]
    [SerializeField] private int taskReward;                            //How many coins are given to play once task is completed
    [Range(0f, 2f)][SerializeField] private float taskTTC;              //How long will the task take to complete
    [Range(0f, 2f)] [SerializeField] private float taskCooldown;        //How long before task can be started again
    [SerializeField] private Treasury treasury;
    [SerializeField] private Transform cartSpawnLocation;
    [SerializeField] private CoinCart cart;
    private bool _onCooldown;
    private Coroutine _onGoingTask;

    protected override void Interact()
    {
        if (_onCooldown == true) return;
        if (_onGoingTask != null) return;
        _onGoingTask = StartCoroutine(OnGoingTask());
    }

    protected override void Cancel()
    {
        if (_onGoingTask == null) return;
        StopCoroutine(_onGoingTask);
        StartCoroutine(Cooldown());
    }

    private IEnumerator OnGoingTask()
    {
        while(true)
        {
            yield return new WaitForSeconds(taskTTC);
            for(int i = 0; i < taskReward; i++)
            {
                SpawnCoinCart();
            }

            if (taskCooldown == 0) continue;
            StartCoroutine(Cooldown());
            break;
        }
    }

    private void SpawnCoinCart()
    {
        CoinCart spawnedCart = Instantiate(cart.gameObject, cartSpawnLocation.position, Quaternion.identity, transform).GetComponent<CoinCart>();
        spawnedCart.Spawned(treasury, treasury.transform, false);
    }

    private IEnumerator Cooldown()
    {
        _onGoingTask = null;
        _onCooldown = true;
        yield return new WaitForSeconds(taskCooldown);
        _onCooldown = false;
    }
}
