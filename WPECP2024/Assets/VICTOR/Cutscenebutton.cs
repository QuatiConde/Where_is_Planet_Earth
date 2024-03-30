using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenebutton : MonoBehaviour
{
    // Lista de objetos de jogo para desativar
    public GameObject[] objetosParaDesativar;

    // Objeto de jogo para ativar
    public GameObject objetoParaAtivar;

    private void Start()
    {
      
    }

    public void DesativarObjetos()
    {
        // Percorre cada objeto de jogo na lista e o desativa
        foreach (GameObject obj in objetosParaDesativar)
        {
            obj.SetActive(false);
        }
    }

    public void AtivarObjeto()
    {
        // Ativa o objeto desejado
        objetoParaAtivar.SetActive(true);
    }

    public void CarregarCena(string nomeDaCena)
    {
        // Carrega a cena com o nome especificado
        SceneManager.LoadScene(nomeDaCena);
    }
}
