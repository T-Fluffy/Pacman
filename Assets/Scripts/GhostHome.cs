using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GhostExtensions
{
    public static void SetPosition(this Ghost ghost, Vector3 position)
    {
        ghost.transform.position = position;
    }
}

public class GhostHome : GhostBehaviour
{
    public Transform inside;
    public Transform outside;

    
    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        // Check for active self to prevent error when object is destroyed
        if (gameObject.activeInHierarchy) {
            StartCoroutine(ExitTransition());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction everytime the ghost hits a wall to create the
        // effect of the ghost bouncing around the home
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            ghost.movement.SetDirection(-ghost.movement.direction);
        }
    }
    private IEnumerator ExitTransition()
    {
        if (this.ghost != null && this.ghost.movement != null)
        {
        // Turn off movement while we manually animate the position
        ghost.movement.SetDirection(Vector2.up, true);
        ghost.movement.GetComponent<Rigidbody2D>().isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Turn on movement and enable physics
        ghost.movement.GetComponent<Rigidbody2D>().isKinematic = false;
        ghost.movement.enabled = true;
        }
    }
}
