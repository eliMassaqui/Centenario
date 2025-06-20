using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Nomes das cenas")]
    public string nomeCenaJogo = "CenaJogo";

    [Header("Textos de estatísticas")]
    public TextMeshProUGUI textoRecorde;
    public TextMeshProUGUI textoTempoTotal;
    public TextMeshProUGUI textoMarcos;
    public TextMeshProUGUI textoJogadas;

    [Header("Estatísticas da última corrida")]
    public TextMeshProUGUI textoUltimaDistancia;
    public TextMeshProUGUI textoUltimoTempo;
    public TextMeshProUGUI textoUltimosMarcos;

    [Header("Mensagem especial")]
    public TextMeshProUGUI textoMensagemEspecial;

    void Start()
    {
        // Estatísticas acumuladas
        float recorde = PlayerPrefs.GetFloat("Recorde", 0f);
        float tempoTotal = PlayerPrefs.GetFloat("TempoTotal", 0f);
        int totalMarcos = PlayerPrefs.GetInt("TotalMarcos", 0);
        int jogadas = PlayerPrefs.GetInt("Jogadas", 0);

        textoRecorde.text = "Recorde: " + Mathf.FloorToInt(recorde) + "m";
        textoTempoTotal.text = "Tempo Total: " + FormatadorTempo(tempoTotal);
        textoMarcos.text = "Marcos Alcançados: " + totalMarcos;
        textoJogadas.text = "Jogadas: " + jogadas;

        // Estatísticas da última corrida
        float ultimaDistancia = PlayerPrefs.GetFloat("UltimaDistancia", 0f);
        float ultimoTempo = PlayerPrefs.GetFloat("UltimoTempo", 0f);
        int ultimosMarcos = PlayerPrefs.GetInt("UltimosMarcos", 0);

        textoUltimaDistancia.text = "Última Distância: " + Mathf.FloorToInt(ultimaDistancia) + "m";
        textoUltimoTempo.text = "Último Tempo: " + FormatadorTempo(ultimoTempo);
        textoUltimosMarcos.text = "Últimos Marcos: " + ultimosMarcos;

        textoMensagemEspecial.text = GerarMensagemEspecial(recorde, jogadas, totalMarcos);
    }

    public void Jogar()
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }


    public void Sair()
    {
        Debug.Log("Sair do Jogo...");
        Application.Quit();
    }

    string FormatadorTempo(float t)
    {
        int min = Mathf.FloorToInt(t / 60f);
        int seg = Mathf.FloorToInt(t % 60f);
        return string.Format("{0:00}:{1:00}", min, seg);
    }

    string GerarMensagemEspecial(float recorde, int jogadas, int marcos)
    {
        if (recorde >= 1000)
            return "Incrível! Você ultrapassou 1.000 metros!";
        else if (recorde >= 500)
            return "Muito bem! Continue superando seus limites!";
        else if (marcos >= 10)
            return "Persistência é a chave. Continue avançando!";
        else if (jogadas >= 5)
            return "Cada tentativa te torna mais forte!";
        else
            return "Bem-vindo de volta! Pronto para mais uma corrida?";
    }
}
