using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public enum States { Start, Game, End, Win, Lose };

public class Rule_ : MonoBehaviour {
    public StateMachine<States> fsm;

    void Awake() {
        fsm = StateMachine<States>.Initialize(this);
        fsm.ChangeState(States.Game);
    }
}
