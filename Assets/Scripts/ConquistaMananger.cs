using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
///  ConquistaManager
///  • Desbloqueia conquistas de máscaras (qualquer quantidade).  
///  • Desbloqueia títulos por distância (qualquer quantidade).  
///  • Mantém um texto permanente que mostra recorde + título atual.  
///  ► **Todos os campos a seguir aparecem no Inspector**.  
/// </summary>
public class ConquistaManager : MonoBehaviour
{
    /* ───────────────────────── 1) Conquistas de Máscaras ───────────────────────── */
    [Header("➊ CONQUISTAS DE MÁSCARAS")]
    public Selectable[] itensMascara;        // Arraste N botões/imagens
    public int[]    limitesMascara =         // Limites correspondentes
        { 10, 30, 65, 100, 500 };
    public string[] nomesMascara =           // Nomes correspondentes
    {
        "Descobridor",
        "Explorador",
        "Guardião Cultural",
        "Colecionador",
        "Mestre das Máscaras"
    };

    /* ───────────────────────── 2) Títulos por Distância ───────────────────────── */
    [Header("➋ TÍTULOS POR DISTÂNCIA")]
    public Selectable[] itensTitulo;         // Arraste M botões/imagens
    public float[] limitesMetros =           // Distâncias em metros
        { 500, 1000, 1500, 2000, 2500,
          3000, 3500, 4000, 4500, 5000 };
    public string[] titulos =
    {
        "Novato", "Turista", "Explorador", "Viajante", "Corajoso",
        "Intrépido", "Mestre", "Veterano", "Lendário", "Ícone"
    };

    /* ───────────────────────── 3) Texto Permanente ───────────────────────── */
    [Header("➌ TEXTO PERMANENTE")]
    public TextMeshProUGUI textoStatus;      // Arraste o TMP da mensagem

    /* ───────────────────────── 4) Cores Gerais ───────────────────────── */
    [Header("➍ CORES DO UI")]
    public Color corDesbloqueado = Color.white;
    public Color corBloqueado    = new(0.4f, 0.4f, 0.4f);

    /* ─────────────────────────  Estado Interno  ───────────────────────── */
    int indiceTituloAtual = -1;

    /* ==================================================================== */
    void Start()
    {
        AtualizarMascara();
        AtualizarTitulos(forceUI : true);    // força mensagem ao iniciar
    }

    void Update()                            // checa novo recorde a cada frame
    {
        AtualizarTitulos(forceUI : false);
    }

    /* ---------- Conquistas de Máscaras ---------- */
    void AtualizarMascara()
    {
        int total = PlayerPrefs.GetInt("TotalMascaras", 0);
        int n = Mathf.Min(itensMascara.Length,
                          limitesMascara.Length,
                          nomesMascara.Length);

        for (int i = 0; i < n; i++)
        {
            bool ok = total >= limitesMascara[i];
            itensMascara[i].interactable = ok;

            if (itensMascara[i].TryGetComponent(out Image img))
                img.color = ok ? corDesbloqueado : corBloqueado;

            if (itensMascara[i].GetComponentInChildren<TextMeshProUGUI>() is { } txt)
            {
                txt.text  = nomesMascara[i];
                txt.color = ok ? corDesbloqueado : corBloqueado;
            }
        }
    }

    /* ---------- Títulos por Distância ---------- */
    void AtualizarTitulos(bool forceUI)
    {
        float recorde = PlayerPrefs.GetFloat("Recorde", 0f);

        // Descobre índice do maior título atingido
        int novoIndice = -1;
        for (int i = limitesMetros.Length - 1; i >= 0; i--)
            if (recorde >= limitesMetros[i]) { novoIndice = i; break; }

        // Se mudou algo ou é update inicial, refaz UI
        if (novoIndice != indiceTituloAtual || forceUI)
        {
            indiceTituloAtual = novoIndice;
            AtualizarTextoStatus(recorde);
            AtualizarIconesTitulos(recorde);
        }
    }

    void AtualizarIconesTitulos(float recorde)
    {
        int m = Mathf.Min(itensTitulo.Length, limitesMetros.Length, titulos.Length);

        for (int i = 0; i < m; i++)
        {
            bool ok = recorde >= limitesMetros[i];
            itensTitulo[i].interactable = ok;

            if (itensTitulo[i].TryGetComponent(out Image img))
                img.color = ok ? corDesbloqueado : corBloqueado;

            if (itensTitulo[i].GetComponentInChildren<TextMeshProUGUI>() is { } txt)
            {
                txt.text  = titulos[i];
                txt.color = ok ? corDesbloqueado : corBloqueado;
            }
        }
    }

    /* ---------- Texto Permanente ---------- */
    void AtualizarTextoStatus(float metros)
    {
        if (textoStatus == null) return;

        string tituloAtual = indiceTituloAtual >= 0 && indiceTituloAtual < titulos.Length
                             ? titulos[indiceTituloAtual]
                             : "Sem título";

        textoStatus.text =
            $"Você percorreu <b>{Mathf.FloorToInt(metros)} m</b>\n" +
            $"Ganhou título de <color=#FFD700><b>{tituloAtual}</b></color>!";
    }
}
