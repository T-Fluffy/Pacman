using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform inside;
    public Transform outside;

    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        if (this.ghost != null)
        {
            StartCoroutine(ExitTransition());
        }
        else
        {
            // Handle the case where 'ghost' is null, perhaps log a warning.
            Debug.LogWarning("Ghost reference is null in GhostHome script.");
        } 
    }
    private IEnumerator ExitTransition(){
        this.ghost.movement.SetDirection(Vector2.up,true);
        this.ghost.movement.rigidBody.isKinematic=true;
        this.ghost.movement.enabled=false;
        
        //current position of the ghost:
        Vector3 position=this.transform.position;
        float duration=0.5f;
        float elapsed=0.0f;

        while (elapsed<duration)
        {
            Vector3 newposition=Vector3.Lerp(position,this.inside.position,elapsed/duration);
            newposition.z=position.z;
            this.ghost.transform.position=newposition;
            elapsed+=Time.deltaTime;
            yield return null;
        }

        //resset elapsed time :
        elapsed=0.0f;

        while (elapsed<duration)
        {
            Vector3 newposition=Vector3.Lerp(this.inside.position,this.outside.position,elapsed/duration);
            newposition.z=position.z;
            this.ghost.transform.position=newposition;
            elapsed+=Time.deltaTime;
            yield return null;
        }
        this.ghost.movement.SetDirection(new Vector2(Random.value<0.5f ? -1.0f : 1.0f, 0.0f),true);
        this.ghost.movement.rigidBody.isKinematic=false;
        this.ghost.movement.enabled=true;
    }
}
