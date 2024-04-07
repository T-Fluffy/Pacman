using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position=other.transform.position;
        position.x=this.connection.position.x;
        position.y=this.connection.position.y; 
        other.transform.position=position;
    }
}
