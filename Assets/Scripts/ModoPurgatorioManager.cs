using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ModoPurgatorioManager : MonoBehaviour
{
    [Header("Referências")]
    public Image filtroTela;
    public TextMeshProUGUI textoDesafio;
    public GameObject painelDerrota;

    [Header("Jogador e HUD")]
    public Transform jogador;
    public float velocidadeSimulada = 10f;

    [Header("Configuração do Purgatório")]
    public Color corPurgatorio = Color.red;
    [Range(0f, 1f)] public float opacidadeNormal = 0.5f;
    public float intervaloPurgatorio = 50f;
    public float tempoParaResponder = 4f;
    public int letrasPorDesafio = 3;

    [Header("Teste Manual")]
    public bool testarNoInspector = false;

    private float distanciaAcumulada = 0f;
    private float proximaAtivacao = 50f;
    private float tempoRestante = 0f;
    private bool emPurgatorio = false;
    private bool esperandoParaIniciarDesafio = false;
    private float tempoEntradaModo = 0f;
    public float tempoCarencia = 0.2f;

    private List<char> letrasPendentes = new List<char>();
    private string baseLetras = "SERRADALEBA";
    private Color corOriginal;

    void Start()
    {
        if (filtroTela != null)
        {
            corOriginal = filtroTela.color;
            filtroTela.gameObject.SetActive(false);
        }

        if (textoDesafio != null)
            textoDesafio.gameObject.SetActive(false);

        if (painelDerrota != null)
            painelDerrota.SetActive(false);
    }

    void Update()
    {
        float delta = Time.deltaTime;
        distanciaAcumulada += velocidadeSimulada * delta;

        if (!emPurgatorio && (distanciaAcumulada >= proximaAtivacao || testarNoInspector))
        {
            testarNoInspector = false;
            proximaAtivacao += intervaloPurgatorio;
            AtivarModoPurgatorio();
        }

        if (emPurgatorio)
        {
            if (!esperandoParaIniciarDesafio)
            {
                tempoRestante -= Time.unscaledDeltaTime;
                VerificarTeclas();

                if (tempoRestante <= 0)
                {
                    Falhar();
                }
            }
        }
    }

    void AtivarModoPurgatorio()
    {
        emPurgatorio = true;
        esperandoParaIniciarDesafio = true;
        Time.timeScale = 0.2f;

        if (filtroTela != null)
        {
            filtroTela.color = new Color(corPurgatorio.r, corPurgatorio.g, corPurgatorio.b, opacidadeNormal);
            filtroTela.gameObject.SetActive(true);
        }

        textoDesafio.text = "MODO PURGATÓRIO";
        textoDesafio.gameObject.SetActive(true);

        Invoke(nameof(IniciarDesafio), 0.2f); // Espera 2 segundos antes de mostrar as letras
    }

    void IniciarDesafio()
    {
        esperandoParaIniciarDesafio = false;
        tempoEntradaModo = Time.unscaledTime;
        tempoRestante = tempoParaResponder;

        letrasPendentes.Clear();
        HashSet<char> usadas = new HashSet<char>();
        while (letrasPendentes.Count < letrasPorDesafio)
        {
            char l = baseLetras[Random.Range(0, baseLetras.Length)];
            if (usadas.Add(l))
                letrasPendentes.Add(l);
        }

        AtualizarTexto();
    }

    void VerificarTeclas()
    {
        if (esperandoParaIniciarDesafio)
            return;

        if (Time.unscaledTime - tempoEntradaModo < tempoCarencia)
            return;

        if (!Input.anyKeyDown) return;

        foreach (char c in Input.inputString)
        {
            char tecla = char.ToUpper(c);

            if (letrasPendentes.Contains(tecla))
            {
                letrasPendentes.Remove(tecla);
                AtualizarTexto();

                if (letrasPendentes.Count == 0)
                    FinalizarModoPurgatorio();
            }
            else
            {
                Falhar();
                return;
            }
        }
    }

    void AtualizarTexto()
    {
        textoDesafio.text = "";
        foreach (char l in letrasPendentes)
            textoDesafio.text += l + " ";
    }

    void Falhar()
    {
        Time.timeScale = 0f;

        if (filtroTela != null)
            filtroTela.color = new Color(corPurgatorio.r, corPurgatorio.g, corPurgatorio.b, 1f);

        textoDesafio.text = "FALHOU!";

        if (painelDerrota != null)
            painelDerrota.SetActive(true);
    }

    void FinalizarModoPurgatorio()
    {
        emPurgatorio = false;
        Time.timeScale = 1f;

        if (filtroTela != null)
        {
            filtroTela.color = corOriginal;
            filtroTela.gameObject.SetActive(false);
        }

        textoDesafio.gameObject.SetActive(false);
    }
}
