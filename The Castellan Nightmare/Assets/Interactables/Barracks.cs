using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Interactable
{
    protected override void Interact()
    {
        CoinsManager.Coins--;
        print("Solder's Hired");
    }
}
