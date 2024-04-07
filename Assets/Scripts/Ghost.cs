using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement{get;private set;}
    public GhostHome home{get;private set;}
    public GhostScatter scatter{get;private set;}
    public GhostChase chase{get;private set;}
    public GhostFrightened frightened{get;private set;}
    public GhostBehaviour initialBehaviour;
    public Transform target;
    public int points=200;

    private void Awake(){
        this.movement=GetComponent<Movement>();
        this.home=GetComponent<GhostHome>();
        this.scatter=GetComponent<GhostScatter>();
        this.chase=GetComponent<GhostChase>();
        this.frightened=GetComponent<GhostFrightened>();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        ResetState();
    }
    public void ResetState(){
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Disable();

        if (this.home!=this.initialBehaviour){}
        {
            this.home.Disable();
        }
        if (this.initialBehaviour!=null)
        {
            this.initialBehaviour.Enable();
        }
    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
