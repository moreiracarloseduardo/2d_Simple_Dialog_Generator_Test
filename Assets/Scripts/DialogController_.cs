using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController_ : MonoBehaviour {
    public float interactionDistance = 1.5f;
    private TextMeshProUGUI promptText;
    private Transform playerTransform;

    private TMP_Text dialogueText;
    private int currentLine = 0;
    private bool isDialogueActive = false;
    private bool waitForPlayerInput = false;
    [SerializeField] private string[] dialogueLines; 
    [SerializeField] Sprite currentAvatar; 

    private GameObject promptTextInstance;
    

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;

        // Crie uma instância do promptText para este NPC como filho do Canvas
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
        Game_.instance.ui.avatar.sprite = currentAvatar;
        Game_.instance.ui.dialoguePanelObject.SetActive(true);
        promptTextInstance.SetActive(false);
        currentLine = 0;
        StartCoroutine(TypewriterEffect(dialogueLines[currentLine]));
    }

    void AdvanceDialogue() {
        if (waitForPlayerInput) {
            currentLine++;

            if (currentLine < dialogueLines.Length) {
                StartCoroutine(TypewriterEffect(dialogueLines[currentLine]));
            } else {
                EndDialogue();
            }
        }
    }

    void EndDialogue() {
        isDialogueActive = false;
        Game_.instance.ui.dialoguePanelObject.SetActive(false);
        promptTextInstance.SetActive(true);
    }

    IEnumerator TypewriterEffect(string text) {
        waitForPlayerInput = false;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(3f);
        waitForPlayerInput = true;
        AdvanceDialogue();
    }
    void OnDestroy() {
        // Destrua a instância do promptText quando o NPC for destruído
        if (promptTextInstance != null) {
            Destroy(promptTextInstance);
        }
    }

}
