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
            UpdateCoinsUI();
        }
    }

    void Awake() {
        fsm = StateMachine<States>.Initialize(this);
        fsm.ChangeState(States.Game);
    }
    private void UpdateCoinsUI() {
        Game_.instance.ui.coinsTotalText.text = coins.ToString();
    }
}
