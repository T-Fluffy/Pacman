using UnityEngine;

public class PowerPallet : Pallet
{
    public float duration = 8f;

    protected override void Eat()
    {
        GameManager.Instance.PowerPalletEaten(this);
    }

}
