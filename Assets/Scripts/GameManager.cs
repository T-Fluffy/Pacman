using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pallets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text gameOver2Text;
    [SerializeField] private Text gameOver3Text;
    [SerializeField] private Button quitButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private int ghostMultiplier = 1;
    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
        if (!HasRemainingPallets()) 
        {
            StartCoroutine(ShowGameOver2Text());
        }
    }
    private IEnumerator ShowGameOver2Text()
    {
        gameOver2Text.enabled = true;
        gameOver3Text.enabled = true;
        yield return new WaitForSeconds(30f);
        NewGame();
    }
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        gameOver2Text.enabled = false;
        gameOver3Text.enabled = false;
    }

    private void NewRound()
    {
        gameOverText.enabled = false;
        gameOver2Text.enabled = false;
        gameOver3Text.enabled = false;
        foreach (Transform pallet in pallets) {
            pallet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].ResetState();
        }
        pacman.ResetState();
    }

    private void GameOver()
    {
        if (lives <= 0)
        {
            gameOverText.enabled = true;
            gameOver3Text.enabled = true;
        }
        else if (!HasRemainingPallets())
        {
            gameOver2Text.enabled = true;
            gameOver3Text.enabled = true;
        }
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }
        quitButton.enabled=true;
        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
         if (scoreText != null)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }
    else
    {
        Debug.LogError("scoreText is null. Make sure it is properly referenced in the GameManager script.");
    }
    }

    public void PacmanEaten()
    {
        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void palletEaten(Pallet pallet)
    {
        pallet.gameObject.SetActive(false);

        SetScore(score + pallet.points);

        if (!HasRemainingPallets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPalletEaten(PowerPallet pallet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(pallet.duration);
        }

        palletEaten(pallet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pallet.duration);
    }

    private bool HasRemainingPallets()
    {
        foreach (Transform pallet in pallets)
        {
            if (pallet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
    public void Exit(){
        Application.Quit();
    }
}