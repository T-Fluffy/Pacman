using UnityEngine;

public class PowerPallet : Pallet
{
    public float duration = 8.0f;
    protected override void Eat(){
        FindObjectOfType<GameManager>().PowerPalletEaten(this);
    }
}
