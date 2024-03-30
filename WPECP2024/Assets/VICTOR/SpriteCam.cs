using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCam : MonoBehaviour

{
    

    void Update()
    {
        if (Camera.main != null)
        {
            Vector3 direction = Camera.main.transform.position - transform.position;
            direction.y = 0; // Ignora a variação no eixo Y
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }
    }
}
