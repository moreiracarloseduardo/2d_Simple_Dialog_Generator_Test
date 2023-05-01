using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public class DialogueEntry {
    public bool isPlayer;
    public string text;
}

public class DialogController_ : MonoBehaviour {
    public float interactionDistance = 1.5f;
    public event Action OnDialogueEnd;
    private Transform playerTransform;

    private TMP_Text dialogueText;
    private int currentLine = 0;
    private bool isDialogueActive = false;
    private bool waitForPlayerInput = false;
    private bool firstDialogueCompleted = false;
    [SerializeField] private DialogueEntry[] dialogueEntries;
    [SerializeField] private DialogueEntry[] secondDialogueEntries;
    [SerializeField] private Sprite npcAvatar;
    [SerializeField] private Sprite playerAvatar;

    private GameObject promptTextInstance;

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;

        Canvas canvas = FindObjectOfType<Canvas>();
        promptTextInstance = Instantiate(Game_.instance.ui.promptTextPrefab, Game_.instance.ui.promptTextPrefab.transform.position, Quaternion.identity);
        promptTextInstance.transform.SetParent(canvas.transform, false);
        promptTextInstance.SetActive(false);

        dialogueText = Game_.instance.ui.dialogueText;
        Game_.instance.ui.dialoguePanelObject.SetActive(false);
    }

    void Update() {
        CheckForPlayerInteraction();
    }

    void CheckForPlayerInteraction() {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance) {
            promptTextInstance.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return)) {
                if (!isDialogueActive) {
                    StartDialogue();
                } else {
                    AdvanceDialogue();
                }
            }
        } else {
            promptTextInstance.SetActive(false);
        }
    }

    void StartDialogue() {
        isDialogueActive = true;
        Game_.instance.rule_.fsm.ChangeState(States.Dialogue);
        Game_.instance.ui.avatar.sprite = dialogueEntries[currentLine].isPlayer ? playerAvatar : npcAvatar;
        Game_.instance.ui.dialoguePanelObject.SetActive(true);
        promptTextInstance.SetActive(false);
        StartCoroutine(TypewriterEffect(dialogueEntries[currentLine].text));

    }

    void AdvanceDialogue() {
        if (waitForPlayerInput) {
            currentLine++;

            if (currentLine < dialogueEntries.Length) {
                Game_.instance.ui.avatar.sprite = dialogueEntries[currentLine].isPlayer ? playerAvatar : npcAvatar;
                StartCoroutine(TypewriterEffect(dialogueEntries[currentLine].text));
            } else {
                EndDialogue();
            }
        }
    }

    void EndDialogue() {
        isDialogueActive = false;
        Game_.instance.ui.dialoguePanelObject.SetActive(false);
        promptTextInstance.SetActive(true);
        OnDialogueEnd?.Invoke();
        currentLine = 0;
        if (!firstDialogueCompleted) {
            firstDialogueCompleted = true;
            dialogueEntries = secondDialogueEntries;
        }
        Game_.instance.rule_.fsm.ChangeState(States.Game);
    }


    IEnumerator TypewriterEffect(string text) {
        waitForPlayerInput = false;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(2f);
        waitForPlayerInput = true;
        AdvanceDialogue();
    }

    void OnDestroy() {
        if (promptTextInstance != null) {
            Destroy(promptTextInstance);
        }
    }

}
