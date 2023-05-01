using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public enum States { Start, Game, End, Win, Lose, Dialogue, Shop, Pause };

public class Rule_ : MonoBehaviour {
    public StateMachine<States> fsm;
    private int coins;
    public int Coins {
        get { return coins; }
        set {
            coins = value;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
            Game_.instance.ui.coinsTotalText.text = coins.ToString();
        }
    }

    void Awake() {
        fsm = StateMachine<States>.Initialize(this);
        fsm.ChangeState(States.Start);
    }
    void Start() {
        LoadCoins();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (fsm.State == States.Game) {
                PauseGame();
            } else if (fsm.State == States.Pause) {
                ResumeGame();
            }
        }
    }
    private void LoadCoins() {
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
