using UnityEngine;

public class TutorialDialogs : MonoBehaviour
{
    private GameObject dialogueBox;
    [SerializeField] private int dialogueIndex;

    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();

        dialogueBox = canvas.gameObject;
        VerifyDialogue();
    }

    void Update()
    {
        if (
            dialogueBox.activeSelf &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        )
        {
            dialogueBox.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void VerifyDialogue()
    {
        if (dialogueIndex == 1)
        {
            dialogueBox.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
