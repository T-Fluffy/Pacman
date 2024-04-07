using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pallets;
    [field: SerializeField] public int score {get;private set;}
    [field: SerializeField] public int lives {get; private set;}
    public int ghostMultiplier {get; private set;} = 1;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        NewGame();
    }

    private void NewGame(){
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void SetScore(int score){
        this.score = score;
    }
    private void SetLives(int lives){
        this.lives = lives;
    }
    private void NewRound(){
        foreach (Transform pallet in this.pallets)
        {
            pallet.gameObject.SetActive(true);
        }
        SetStates(true);
    }
    private void Update(){
        if (this.lives<=0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }
    private void ResetStates(){
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
    }
    /*********************************************************
        Setting the Ghost and Pacman states ingame.
        ( true == active && false == unactive)
    *********************************************************/
    private void SetStates(bool state){
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(state);
        }
        this.pacman.gameObject.SetActive(state);
    }
    private void GameOver(){
        SetStates(false);
    }
    public void GhostEaten(Ghost ghost){
        int points=ghost.points*this.ghostMultiplier;
        SetScore(this.score+points);
        this.ghostMultiplier++;
    }
    public void PacmanEaten(){
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives-1);
        if (this.lives>0)
        {
            Invoke(nameof(ResetStates),3.0f);
        }else{
            GameOver();
        }
    }
    public void PalletEaten(Pallet pallet){
        pallet.gameObject.SetActive(false);
        SetScore(this.score + pallet.points);
        if (!HasRemainingPallet())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound),3.0f);
        }
    }
    public void PowerPalletEaten(PowerPallet pallet){

        Invoke(nameof(ResetGhostMultiplier),pallet.duration);
        CancelInvoke();
        PalletEaten(pallet);
    }
    private bool HasRemainingPallet(){
        foreach (Transform pallet in this.pallets)
        {
            if (pallet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    private void ResetGhostMultiplier(){
        this.ghostMultiplier = 1;
    }

}
