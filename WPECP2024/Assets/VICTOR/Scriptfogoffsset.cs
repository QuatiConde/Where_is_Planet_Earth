using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptfogoffsset : MonoBehaviour
{
    private Vector3 offset;

    void Start()
    {
        // Calcula e armazena o deslocamento entre o objeto e a c�mera
        offset = transform.position - Camera.main.transform.position;
    }

    void LateUpdate()
    {
        // Atualiza a posi��o do objeto para seguir a c�mera, mantendo o deslocamento
        transform.position = Camera.main.transform.position + offset;
    }
}
