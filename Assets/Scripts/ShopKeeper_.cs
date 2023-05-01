using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopKeeper_ : MonoBehaviour {
    private DialogController_ dialogController;

    void Start() {
        // Set up the dialogController reference and add the OpenShop method to the OnDialogueEnd event
        dialogController = GetComponent<DialogController_>();
        dialogController.OnDialogueEnd += OpenShop;
    }

    void OnDestroy() {
        // Remove the OpenShop method from the OnDialogueEnd event
        dialogController.OnDialogueEnd -= OpenShop;
    }

    private void OpenShop() {
        // Open the shop UI and change the game state to Shop when the dialogue ends
        Game_.instance.ui.MyItemsPanelObject.SetActive(true);
        Game_.instance.rule_.fsm.ChangeState(States.Shop);
        Game_.instance.ui.shopObject.SetActive(true);
    }

}
