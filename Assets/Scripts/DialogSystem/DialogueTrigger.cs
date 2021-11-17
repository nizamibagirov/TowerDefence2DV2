using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public ObjectDialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
