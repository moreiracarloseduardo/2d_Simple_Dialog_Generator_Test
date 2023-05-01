using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public enum States { Start, Game, End, Win, Lose, Dialogue, Shop, Pause }; // Enumeration of possible game states

public class Rule_ : MonoBehaviour {
    public StateMachine<States> fsm; // State machine to control the game flow
    private int coins; // Variable to store player's coin count
    public int Coins {
        get { return coins; }
        set {
            coins = value;
            PlayerPrefs.SetInt("Coins", coins); // Save coins to PlayerPrefs
            PlayerPrefs.Save(); // Persist changes in PlayerPrefs
            Game_.instance.ui.coinsTotalText.text = coins.ToString(); // Update the UI with the new coin count
        }
    }

    void Awake() {
        fsm = StateMachine<States>.Initialize(this); // Initialize the state machine
        fsm.ChangeState(States.Start); // Set the initial state to 'Start'
    }
    void Start() {
        LoadCoins(); // Load coins from PlayerPrefs
    }
    void Update() {
        // Handle the 'Escape' key press to pause or resume the game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (fsm.State == States.Game) {
                PauseGame();
            } else if (fsm.State == States.Pause) {
                ResumeGame();
            }
        }
    }
    private void LoadCoins() {
        // Load coins from PlayerPrefs or set to default value if not present
        if (PlayerPrefs.HasKey("Coins")) {
            Coins = PlayerPrefs.GetInt("Coins");
        } else {
            Coins = 10;
        }
    }

    void Start_Enter() {
    }
    public void StartGame() {
        fsm.ChangeState(States.Game); 
        Game_.instance.ui.startUi.SetActive(false);
    }
    public void PauseGame() {
        fsm.ChangeState(States.Pause); 
    }
    void Pause_Enter() {
        Game_.instance.ui.pauseUi.SetActive(true);
        Time.timeScale = 0f;
    }

    void Pause_Exit() {
        Game_.instance.ui.pauseUi.SetActive(false); 
        Time.timeScale = 1f; 
    }
    public void ResumeGame() {
        fsm.ChangeState(States.Game); 
    }
}
