using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node=other.GetComponent<Node>();
        
        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            // find the shortest path to pacman to chase him
            Vector2 direction=Vector2.zero;
            float minDistance=float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition= this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance=(this.ghost.target.position - newPosition).sqrMagnitude;
                if (distance < minDistance)
                {
                    direction=availableDirection;
                    minDistance=distance;    
                }
            }
            this.ghost.movement.SetDirection(direction);
        }
    }
}
