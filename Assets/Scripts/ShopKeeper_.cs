using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopKeeper_ : MonoBehaviour {
    public float interactionDistance = 1.5f;
    private TextMeshProUGUI promptText;
    private Transform playerTransform;

    private TMP_Text dialogueText;
    private int currentLine = 0;
    private bool isDialogueActive = false;

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        promptText = Game_.instance.ui.promptTextObject.GetComponentInChildren<TextMeshProUGUI>();
        promptText.enabled = false;

        dialogueText = Game_.instance.ui.dialoguePanelObject.transform.Find("DialogueText").GetComponent<TMP_Text>();
        Game_.instance.ui.dialoguePanelObject.SetActive(false);
    }

    void Update() {
        CheckForPlayerInteraction();
    }

    void CheckForPlayerInteraction() {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance) {
            promptText.enabled = true;

            if (Input.GetKeyDown(KeyCode.Return)) {
                if (!isDialogueActive) {
                    StartDialogue();
                } else {
                    AdvanceDialogue();
                }
            }
        } else {
            promptText.enabled = false;
        }
    }
    void StartDialogue() {
        isDialogueActive = true;
        Game_.instance.ui.dialoguePanelObject.SetActive(true);
        promptText.enabled = false;
        currentLine = 0;
        dialogueText.text = Game_.instance.ui.dialogueLines[currentLine];
    }

    void AdvanceDialogue() {
        currentLine++;

        if (currentLine < Game_.instance.ui.dialogueLines.Length) {
            dialogueText.text = Game_.instance.ui.dialogueLines[currentLine];
        } else {
            EndDialogue();
        }
    }

    void EndDialogue() {
        isDialogueActive = false;
        Game_.instance.ui.dialoguePanelObject.SetActive(false);
        promptText.enabled = true;
    }
}
