using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Orange : MonoBehaviour
{
     public int points = 30;

    protected virtual void Eat()
    {
        GameManager.Instance.OrangeEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
