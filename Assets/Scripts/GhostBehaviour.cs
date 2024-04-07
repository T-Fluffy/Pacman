using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehaviour : MonoBehaviour
{
    public Ghost ghost{ get; private set; }
    public float duration;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }
    public void Enable(){
        Enable(this.duration);
    }
    public virtual void Enable(float duration){
        this.enabled=true;
        CancelInvoke();
        Invoke(nameof(Disable),duration);
    }
    public virtual void Disable(){
        this.enabled=false;
        CancelInvoke();
    }
}
