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

    private float pontoInicialZ;
    private float proximoMarco;
    private float tempoDecorrido;
    private float recorde;

    void Start()
    {
        pontoInicialZ = jogador.position.z;
        proximoMarco = intervaloMarco;
        recorde = PlayerPrefs.GetFloat("Recorde", 0f);

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
    }

    void OnDisable()
    {
        float distanciaFinal = jogador.position.z - pontoInicialZ;
        int marcosAlcancados = Mathf.FloorToInt(distanciaFinal / intervaloMarco);

        // Salva estatísticas acumuladas
        PlayerPrefs.SetFloat("TempoTotal", PlayerPrefs.GetFloat("TempoTotal", 0f) + tempoDecorrido);
        PlayerPrefs.SetInt("TotalMarcos", PlayerPrefs.GetInt("TotalMarcos", 0) + marcosAlcancados);
        PlayerPrefs.SetInt("Jogadas", PlayerPrefs.GetInt("Jogadas", 0) + 1);

        // Salva estatísticas da última corrida
        PlayerPrefs.SetFloat("UltimaDistancia", distanciaFinal);
        PlayerPrefs.SetFloat("UltimoTempo", tempoDecorrido);
        PlayerPrefs.SetInt("UltimosMarcos", marcosAlcancados);
    }

    string FormatadorTempo(float t)
    {
        int minutos = Mathf.FloorToInt(t / 60f);
        int segundos = Mathf.FloorToInt(t % 60f);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
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
