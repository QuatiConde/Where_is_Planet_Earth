using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptfogoffsset : MonoBehaviour
{
    private Vector3 offset;

    void Start()
    {
        // Calcula e armazena o deslocamento entre o objeto e a câmera
        offset = transform.position - Camera.main.transform.position;
    }

    void LateUpdate()
    {
        // Atualiza a posição do objeto para seguir a câmera, mantendo o deslocamento
        transform.position = Camera.main.transform.position + offset;
    }
}
