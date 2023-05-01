using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public enum States { Start, Game, End, Win, Lose };

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
        fsm.ChangeState(States.Game);
    }
    void Start() {
        LoadCoins();
    }
    private void LoadCoins() {
        if (PlayerPrefs.HasKey("Coins")) {
            Coins = PlayerPrefs.GetInt("Coins");
        } else {
            Coins = 60;
        }
    }
}
