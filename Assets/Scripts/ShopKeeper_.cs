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
        Game_.instance.ui.shopObject.SetActive(true);
    }

}
