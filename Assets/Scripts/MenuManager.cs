using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Nomes das cenas")]
    public string nomeCenaJogo = "CenaJogo";

    [Header("Painel principal (pai)")]
    public GameObject painelPrincipal;

    [Header("Filhos específicos")]
    public GameObject filho0;
    public GameObject filho1;
    public GameObject filho2;

    [Header("Painel isolado")]
    public GameObject painelUnico;

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
        float recorde       = PlayerPrefs.GetFloat("Recorde",      0f);
        float tempoTotal    = PlayerPrefs.GetFloat("TempoTotal",   0f);
        int   totalMarcos   = PlayerPrefs.GetInt  ("TotalMarcos",  0);
        int   jogadas       = PlayerPrefs.GetInt  ("Jogadas",      0);
        int   totalMascaras = PlayerPrefs.GetInt  ("TotalMascaras",0);

        textoRecorde.text        = Mathf.FloorToInt(recorde) + " m";
        textoTempoTotal.text     = FormatadorTempo(tempoTotal);
        textoMarcos.text         = totalMarcos.ToString();
        textoJogadas.text        = jogadas.ToString();
        textoMascarasTotais.text = totalMascaras.ToString();

        float ultimaDistancia = PlayerPrefs.GetFloat("UltimaDistancia", 0f);
        float ultimoTempo     = PlayerPrefs.GetFloat("UltimoTempo",     0f);
        int   ultimosMarcos   = PlayerPrefs.GetInt  ("UltimosMarcos",   0);
        int   ultimasMascaras = PlayerPrefs.GetInt  ("UltimasMascaras", 0);

        textoUltimaDistancia.text = Mathf.FloorToInt(ultimaDistancia) + " m";
        textoUltimoTempo.text     = FormatadorTempo(ultimoTempo);
        textoUltimosMarcos.text   = ultimosMarcos.ToString();
        textoUltimasMascaras.text = ultimasMascaras.ToString();

        FecharPainelPrincipal();
        if (painelUnico != null) painelUnico.SetActive(false);
    }

    public void Jogar() => SceneManager.LoadScene(nomeCenaJogo);

    public void Sair()
    {
        Debug.Log("Sair do Jogo...");
        Application.Quit();
    }

    public void AtivarPainelComFilho(int index)
    {
        if (painelPrincipal == null) return;

        painelPrincipal.SetActive(true);

        filho0?.SetActive(false);
        filho1?.SetActive(false);
        filho2?.SetActive(false);

        switch (index)
        {
            case 0: filho0?.SetActive(true); break;
            case 1: filho1?.SetActive(true); break;
            case 2: filho2?.SetActive(true); break;
            default: Debug.LogWarning("Índice de filho inválido."); break;
        }
    }

    public void FecharPainelPrincipal()
    {
        painelPrincipal?.SetActive(false);
        filho0?.SetActive(false);
        filho1?.SetActive(false);
        filho2?.SetActive(false);
    }

    public void AlternarPainelUnico()
    {
        if (painelUnico != null)
            painelUnico.SetActive(!painelUnico.activeSelf);
    }

    string FormatadorTempo(float t)
    {
        int min = Mathf.FloorToInt(t / 60f);
        int seg = Mathf.FloorToInt(t % 60f);
        return $"{min:00}:{seg:00} min";
    }
}
