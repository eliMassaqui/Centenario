using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public Transform jogador;

    [Header("Textos")]
    public TextMeshProUGUI textoDistancia;
    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoRecorde;
    public TextMeshProUGUI textoMarco;

    [Header("Configurações")]
    public float intervaloMarco = 100f;
    public float tempoExibicaoMarco = 2f;

    [Header("Debug PlayerPrefs")]
    public float recordeSalvo;      // Só leitura
    public float novoRecorde = 0f;  // Editável
    public bool salvarNovoRecorde;  // Quando ativado, salva novo recorde
    public bool apagarRecorde;      // Quando ativado, apaga o recorde

    private float pontoInicialZ;
    private float proximoMarco;
    private float tempoDecorrido;
    private float recorde;

    void Start()
    {
        pontoInicialZ = jogador.position.z;
        proximoMarco = intervaloMarco;
        recorde = PlayerPrefs.GetFloat("Recorde", 0f);
        recordeSalvo = recorde;

        textoMarco.gameObject.SetActive(false);
        textoRecorde.gameObject.SetActive(true);
    }

    void Update()
    {
        float distancia = jogador.position.z - pontoInicialZ;
        tempoDecorrido += Time.deltaTime;

        textoDistancia.text = "Distância: " + Mathf.FloorToInt(distancia) + "m";
        textoTempo.text = "Tempo: " + FormatadorTempo(tempoDecorrido);
        textoRecorde.text = "Recorde: " + Mathf.FloorToInt(recorde) + "m";

        if (distancia >= proximoMarco)
        {
            MostrarMarco(Mathf.FloorToInt(proximoMarco) + "m alcançados!");
            proximoMarco += intervaloMarco;
        }

        if (distancia >= recorde)
        {
            recorde = distancia;
            PlayerPrefs.SetFloat("Recorde", recorde);
            textoRecorde.gameObject.SetActive(false);
        }

        // Checar os bools de controle no Inspector
        if (salvarNovoRecorde)
        {
            PlayerPrefs.SetFloat("Recorde", novoRecorde);
            recordeSalvo = novoRecorde;
            Debug.Log("✔ Novo recorde salvo: " + novoRecorde);
            salvarNovoRecorde = false; // Resetar para não repetir
        }

        if (apagarRecorde)
        {
            PlayerPrefs.DeleteKey("Recorde");
            recordeSalvo = 0f;
            Debug.Log("❌ Recorde apagado.");
            apagarRecorde = false; // Resetar para não repetir
        }
    }

    string FormatadorTempo(float t)
    {
        int minutos = Mathf.FloorToInt(t / 60f);
        int segundos = Mathf.FloorToInt(t % 60f);
        return string.Format("Tempo: {0:00}:{1:00}", minutos, segundos);
    }

    void MostrarMarco(string mensagem)
    {
        textoMarco.text = mensagem;
        textoMarco.gameObject.SetActive(true);
        CancelInvoke("OcultarMarco");
        Invoke("OcultarMarco", tempoExibicaoMarco);
    }

    void OcultarMarco()
    {
        textoMarco.gameObject.SetActive(false);
    }
}
