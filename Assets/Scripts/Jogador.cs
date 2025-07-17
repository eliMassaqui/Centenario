using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Jogador : MonoBehaviour
{
    [Header("🎭 Coleta de Máscaras")]
    public int mascarasColetadas = 0;
    public TextMeshProUGUI textoMascaras;

    [Header("💀 Vida")]
    public Image barraVida;
    public Color corVida = new Color(1f, 0f, 0f, 1f);
    public float vidaMaxima = 100f;
    public float vidaAtual = 100f;
    public float danoPorColisao = 100f / 15f;
    public bool estaVivo = true;

    [Header("⚡ Energia - Beber Energético")]
    public Image barraEnergia;
    public Image imagemCura;
    public Color corCura = new Color(1f, 1f, 0f, 0.5f);
    public float Cura = 0f;
    public float tempoParaCarregar = 5f;
    public float tempoParaCurar = 3f;
    public float tempoParaDescarregar = 10f;
    public float percentualCura = 10f;
    public float energiaAtual = 0f;
    public bool carregando = false;
    public bool energiaPronta = false;
    public bool descarregando = false;

    [Header("☠️ Game Over")]
    public GameObject painelGameOver;

    void Start()
    {
        estaVivo = true;
        Time.timeScale = 1f;
        vidaAtual = vidaMaxima;

        mascarasColetadas = PlayerPrefs.GetInt("TotalMascaras", 0);

        if (painelGameOver != null)
            painelGameOver.SetActive(false);

        if (imagemCura != null)
            imagemCura.color = new Color(corCura.r, corCura.g, corCura.b, 0f);

        if (barraVida != null)
            barraVida.color = corVida;

        AtualizarBarras();
        AtualizarTextoMascaras();
    }

    void Update()
    {
        if (!estaVivo) return;

        // Recarregar energia
        if (Input.GetKey(KeyCode.E) && !energiaPronta && !descarregando)
        {
            carregando = true;
            energiaAtual += Time.deltaTime / tempoParaCarregar;
            energiaAtual = Mathf.Clamp01(energiaAtual);

            if (energiaAtual >= 1f)
            {
                energiaPronta = true;
                carregando = false;
                StartCoroutine(AplicarCura());
            }
        }
        else if (!Input.GetKey(KeyCode.E) && !energiaPronta)
        {
            carregando = false;
        }

        AtualizarBarras();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!estaVivo) return;

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            vidaAtual -= danoPorColisao;
            AtualizarBarras();

            if (vidaAtual <= 0f)
            {
                estaVivo = false;
                MostrarGameOver();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!estaVivo) return;

        if (other.CompareTag("Mascara"))
        {
            mascarasColetadas++;
            PlayerPrefs.SetInt("TotalMascaras", mascarasColetadas);
            AtualizarTextoMascaras();
            Destroy(other.gameObject);
        }
    }

    void AtualizarBarras()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = vidaAtual / vidaMaxima;
            barraVida.color = corVida;
        }

        if (barraEnergia != null)
            barraEnergia.fillAmount = energiaAtual;
    }

    void AtualizarTextoMascaras()
    {
        if (textoMascaras != null)
            textoMascaras.text = "Máscaras: " + mascarasColetadas;
    }

    IEnumerator AplicarCura()
    {
        Cura = (vidaMaxima * percentualCura) / 100f;
        vidaAtual += Cura;
        vidaAtual = Mathf.Clamp(vidaAtual, 0f, vidaMaxima);
        AtualizarBarras();

        if (imagemCura != null)
            imagemCura.color = corCura;

        yield return new WaitForSeconds(tempoParaCurar);

        if (imagemCura != null)
            imagemCura.color = new Color(corCura.r, corCura.g, corCura.b, 0f);

        StartCoroutine(DescarregarEnergia());
    }

    IEnumerator DescarregarEnergia()
    {
        descarregando = true;
        float t = 0f;

        while (t < tempoParaDescarregar)
        {
            energiaAtual = Mathf.Lerp(1f, 0f, t / tempoParaDescarregar);
            AtualizarBarras();
            t += Time.deltaTime;
            yield return null;
        }

        energiaAtual = 0f;
        energiaPronta = false;
        descarregando = false;
    }

    void MostrarGameOver()
    {
        Time.timeScale = 0f;

        if (painelGameOver != null)
            painelGameOver.SetActive(true);
    }

    public void Repetir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MENU");
    }
}
