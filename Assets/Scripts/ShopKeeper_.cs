using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopKeeper_ : MonoBehaviour {
    private DialogController_ dialogController;

    void Start() {
        dialogController = GetComponent<DialogController_>();
        dialogController.OnDialogueEnd += OpenShop;
    }

    void OnDestroy() {
        dialogController.OnDialogueEnd -= OpenShop;
    }

    private void OpenShop() {
        Game_.instance.ui.MyItemsPanelObject.SetActive(true);
        Game_.instance.rule_.fsm.ChangeState(States.Shop); 
        Game_.instance.ui.shopObject.SetActive(true);
    }

}
