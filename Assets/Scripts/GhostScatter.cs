using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node=other.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frightened.enabled){
            int index= Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index]== -this.ghost.movement.direction && node.availableDirections.Count>1)
            {
                // increment direction to the next one
                index++;
                if (index>node.availableDirections.Count)
                {   
                    index=0;
                }
            }
            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
