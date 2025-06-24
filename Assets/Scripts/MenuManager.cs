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
    public TextMeshProUGUI textoMascarasTotais;
    public TextMeshProUGUI textoUltimasMascaras;

    [Header("Estatísticas da última corrida")]
    public TextMeshProUGUI textoUltimaDistancia;
    public TextMeshProUGUI textoUltimoTempo;
    public TextMeshProUGUI textoUltimosMarcos;

    void Start()
    {
        // Estatísticas acumuladas
        float recorde = PlayerPrefs.GetFloat("Recorde", 0f);
        float tempoTotal = PlayerPrefs.GetFloat("TempoTotal", 0f);
        int totalMarcos = PlayerPrefs.GetInt("TotalMarcos", 0);
        int jogadas = PlayerPrefs.GetInt("Jogadas", 0);
        int totalMascaras = PlayerPrefs.GetInt("TotalMascaras", 0);

        textoRecorde.text = "Recorde: " + Mathf.FloorToInt(recorde) + "m";
        textoTempoTotal.text = "Tempo Total: " + FormatadorTempo(tempoTotal);
        textoMarcos.text = "Marcos Alcançados: " + totalMarcos;
        textoJogadas.text = "Jogadas: " + jogadas;
        textoMascarasTotais.text = "Máscaras Totais: " + totalMascaras;

        // Última corrida
        float ultimaDistancia = PlayerPrefs.GetFloat("UltimaDistancia", 0f);
        float ultimoTempo = PlayerPrefs.GetFloat("UltimoTempo", 0f);
        int ultimosMarcos = PlayerPrefs.GetInt("UltimosMarcos", 0);
        int ultimasMascaras = PlayerPrefs.GetInt("UltimasMascaras", 0);

        textoUltimaDistancia.text = "Última Distância: " + Mathf.FloorToInt(ultimaDistancia) + "m";
        textoUltimoTempo.text = "Último Tempo: " + FormatadorTempo(ultimoTempo);
        textoUltimosMarcos.text = "Últimos Marcos: " + ultimosMarcos;
        textoUltimasMascaras.text = "Máscaras Coletadas: " + ultimasMascaras;
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
}
