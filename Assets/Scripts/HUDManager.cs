using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoDistancia;
    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoRecorde;
    public TextMeshProUGUI textoMarco;

    [Header("Painel Marco")]
    public GameObject painelMarco;

    [Header("Painel Final (5000m)")]
    public GameObject painelFinal;
    public float tempoFinalAtivo = 10f;

    [Header("Configurações")]
    public float intervaloMarco = 500f;
    public float tempoExibicaoMarco = 2f;
    public float velocidadeSimulada = 10f;

    [Header("Progresso por Recorde")]
    public Image levelBar;
    public float distanciaParaNivelMax = 5000f;

    [Header("Opções de Depuração")]
    public bool resetarRecordeNoStart = false;

    private float distanciaSimulada = 0f;
    private float proximoMarco;
    private float tempoDecorrido;
    private float recorde;
    private bool finalAtivado = false;

    void Start()
    {
        if (resetarRecordeNoStart)
        {
            PlayerPrefs.DeleteKey("Recorde");
            recorde = 0f;
        }
        else
        {
            recorde = PlayerPrefs.GetFloat("Recorde", 0f);
        }

        proximoMarco = intervaloMarco;

        if (painelMarco != null)
            painelMarco.SetActive(false);

        if (painelFinal != null)
            painelFinal.SetActive(false);

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

        if (levelBar != null)
        {
            float progresso = Mathf.Clamp01(recorde / distanciaParaNivelMax);
            levelBar.fillAmount = progresso;
        }

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

        if (!finalAtivado && distanciaSimulada >= 5000f)
        {
            finalAtivado = true;
            if (painelFinal != null)
            {
                painelFinal.SetActive(true);
                Invoke("DesativarFinal", tempoFinalAtivo);
            }
        }
    }

    void DesativarFinal()
    {
        if (painelFinal != null)
            painelFinal.SetActive(false);
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

    public float TempoDecorrido => tempoDecorrido;
    public float DistanciaAtual => distanciaSimulada;
}
