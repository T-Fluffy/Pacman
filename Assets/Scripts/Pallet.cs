using UnityEngine;

public class Pallet : MonoBehaviour
{
    public int points=10;

    protected virtual void Eat(){
        FindObjectOfType<GameManager>().PalletEaten(this);
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
