using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Nome das cenas no Build Settings
    public string nomeCenaJogo = "CenaJogo";
    public string nomeCenaConfig = "CenaConfiguracoes";

    // Botão Jogar
    public void Jogar()
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }

    // Botão Configurações
    public void Configuracoes()
    {
        SceneManager.LoadScene(nomeCenaConfig);
    }

    // Botão Sair
    public void Sair()
    {
        Debug.Log("Sair do Jogo...");
        Application.Quit();
    }
}
