using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { private set; get; }
    
    private void Awake()
    {
        Instance = this;
    }

    public Transform enemySpawner;
}
