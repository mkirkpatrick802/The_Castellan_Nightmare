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

    protected override float Scaler(float value)
    {
        throw new System.NotImplementedException();
    }
}
