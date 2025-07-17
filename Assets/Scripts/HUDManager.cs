using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoDistancia;
    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoRecorde;
    public TextMeshProUGUI textoMarco;

    [Header("Painel Marco")]
    public GameObject painelMarco;

    [Header("Configurações")]
    public float intervaloMarco = 500f;
    public float tempoExibicaoMarco = 2f;
    public float velocidadeSimulada = 10f; // m/s simulados

    private float distanciaSimulada = 0f;
    private float proximoMarco;
    private float tempoDecorrido;
    private float recorde;

    void Start()
    {
        proximoMarco = intervaloMarco;
        recorde = PlayerPrefs.GetFloat("Recorde", 0f);

        if (painelMarco != null)
            painelMarco.SetActive(false);

        if (textoRecorde != null)
            textoRecorde.gameObject.SetActive(true);
    }

    void Update()
    {
        float delta = Time.deltaTime;

        tempoDecorrido += delta;
        distanciaSimulada += velocidadeSimulada * delta;

        if (textoDistancia != null)
            textoDistancia.text = Mathf.FloorToInt(distanciaSimulada) + "m";

        if (textoTempo != null)
            textoTempo.text = FormatadorTempo(tempoDecorrido);

        if (textoRecorde != null)
            textoRecorde.text = Mathf.FloorToInt(recorde) + "m";

        if (distanciaSimulada >= proximoMarco)
        {
            MostrarMarco(Mathf.FloorToInt(proximoMarco) + "m alcançados!");
            proximoMarco += intervaloMarco;
        }

        if (distanciaSimulada >= recorde)
        {
            recorde = distanciaSimulada;
            PlayerPrefs.SetFloat("Recorde", recorde);
            if (textoRecorde != null)
                textoRecorde.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        int marcosAlcancados = Mathf.FloorToInt(distanciaSimulada / intervaloMarco);

        PlayerPrefs.SetFloat("TempoTotal", PlayerPrefs.GetFloat("TempoTotal", 0f) + tempoDecorrido);
        PlayerPrefs.SetInt("TotalMarcos", PlayerPrefs.GetInt("TotalMarcos", 0) + marcosAlcancados);
        PlayerPrefs.SetInt("Jogadas", PlayerPrefs.GetInt("Jogadas", 0) + 1);

        PlayerPrefs.SetFloat("UltimaDistancia", distanciaSimulada);
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
        if (textoMarco != null)
            textoMarco.text = mensagem;

        if (painelMarco != null)
            painelMarco.SetActive(true);

        CancelInvoke("OcultarMarco");
        Invoke("OcultarMarco", tempoExibicaoMarco);
    }

    void OcultarMarco()
    {
        if (painelMarco != null)
            painelMarco.SetActive(false);
    }

    // === Permitir acesso externo ===
    public float TempoDecorrido => tempoDecorrido;
    public float DistanciaAtual => distanciaSimulada;
}
