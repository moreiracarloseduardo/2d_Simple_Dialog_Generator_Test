using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Ui_ : MonoBehaviour {
    [Header("Dialogue Objects References")]
    public GameObject promptTextPrefab;
    public GameObject dialoguePanelObject;
    public TextMeshProUGUI dialogueText;
    public Image avatar;
    [Header("Shop")]
    public GameObject shopObject;
    [Header("Coins")]
    public GameObject coinsObject;
    public TextMeshProUGUI coinsTotalText;
    [Header("My Items Panel")]
    public GameObject MyItemsPanelObject;
    public GameObject MyItemsGridObject;
    public GameObject myItemPrefab;
}
