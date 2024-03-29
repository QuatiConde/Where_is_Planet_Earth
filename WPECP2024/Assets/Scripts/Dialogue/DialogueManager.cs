using UnityEngine;
using System.Collections;
using TMPro;
using NaughtyAttributes;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("References")]
    public Dialogue_SO data;
    [Header("Settings")]
    [Tooltip("Clear the dialogue box after X seconds")] public float clearAfter = 3f;
    public bool useTipeWriterEffect;
    [ShowIf("useTipeWriterEffect")] public float tipeWriterCharDelay = 0.125f;
    [Header("Text")]
    public TMP_Text dialogueText;

    //References
    private Coroutine tipeWriter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void ShowDialogue(int index)
    {
        if (tipeWriter != null)
            StopCoroutine(tipeWriter);

        CancelInvoke(nameof(ClearDialogue));
        ClearDialogue();

        if (!useTipeWriterEffect)
            dialogueText.text = data.dialogues[index].dialogue;
        else
        {
            tipeWriter = StartCoroutine(PlayText(data.dialogues[index].dialogue));
        }        
    }

    public void ClearDialogue()
    {
        dialogueText.text = "";
    }

    private IEnumerator PlayText(string ctx)
    {
        foreach (char c in ctx)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(tipeWriterCharDelay);
        }

        Invoke(nameof(ClearDialogue), clearAfter);
    }
}