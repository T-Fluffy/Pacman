using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        if (ghost!=null)
        {
            return;
        }
        this.ghost.chase.Enable();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Figure out the shortest path to find and chase pacman
        Node node=other.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frightened.enabled){
            int index;
            do{
                index= Random.Range(0, node.availableDirections.Count);
            }while(node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1);
            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
