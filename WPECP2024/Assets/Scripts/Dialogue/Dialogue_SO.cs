using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/New Dialogue")]
public class Dialogue_SO : ScriptableObject
{
    public Dialogue[] dialogues;
}

[Serializable]
public class Dialogue
{
    [Header("ID do Dialogo")]
    public int index;
    [Header("Contexto apenas para sabermos ao que se refere")]
    [TextArea(1, 5)] public string context;
    [Header("Dialogo que ira aparecer na tela")]
    [TextArea(1, 5)] public string dialogue;
}