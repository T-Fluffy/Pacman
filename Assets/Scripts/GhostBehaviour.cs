using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehaviour : MonoBehaviour
{
    protected Ghost ghost{ get; private set; }
    public float duration;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        if(this.ghost!=null){
        this.enabled = false;
        }
    }
    public void Enable(){
        Enable2(this.duration);
    }
    public virtual void Enable2(float duration){
        this.enabled=true;
        CancelInvoke();
        Invoke(nameof(Disable),duration);
    }
    public virtual void Disable(){
        this.enabled=false;
        CancelInvoke();
    }
}
