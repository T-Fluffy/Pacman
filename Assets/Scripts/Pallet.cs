using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pallet : MonoBehaviour
{
 public int points = 10;

    protected virtual void Eat()
    {
        GameManager.Instance.palletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            Eat();
        }
    }
}
