using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Interactable
{
    protected override void Interact()
    {
        Coins.coins--;
        print("Solder's Hired");
    }
}
