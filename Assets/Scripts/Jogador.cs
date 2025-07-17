using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Jogador : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelGameOver;
    public Image barraVida;
    public TextMeshProUGUI textoMascaras;

    private float vida = 100f;
    private float vidaMaxima = 100f;
    private float danoPorColisao = 100f / 6f;

    private int mascarasColetadas = 0;
    public bool estaVivo = true;

    void Start()
    {
        InicializarEstado();
    }

    void OnCollisionEnter(Collision colisao)
    {
        if (!estaVivo) return;

        if (colisao.gameObject.CompareTag("Obstaculo"))
        {
            ReceberDano();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!estaVivo) return;

        if (other.CompareTag("Mascara"))
        {
            ColetarMascara(other.gameObject);
        }
    }

    void ColetarMascara(GameObject mascara)
    {
        mascarasColetadas++;
        Destroy(mascara);
        AtualizarUI();
    }

    public void ReceberDano()
    {
        vida -= danoPorColisao;
        AtualizarBarraVida();

        Debug.Log($"Colisão detectada! Vida restante: {vida:F2}");

        if (vida <= 0)
        {
            vida = 0;
            estaVivo = false;
            MostrarGameOver();
        }
    }

    void InicializarEstado()
    {
        estaVivo = true;
        vida = vidaMaxima;
        Time.timeScale = 1f;

        if (painelGameOver != null)
            painelGameOver.SetActive(false);

        AtualizarBarraVida();
        AtualizarUI();
    }

    void AtualizarBarraVida()
    {
        if (barraVida != null)
            barraVida.fillAmount = vida / vidaMaxima;
    }

    void AtualizarUI()
    {
        if (textoMascaras != null)
            textoMascaras.text = "Mascaras: " + mascarasColetadas;
    }

    void MostrarGameOver()
    {
        Time.timeScale = 0f;

        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        PlayerPrefs.SetInt("TotalMascaras", PlayerPrefs.GetInt("TotalMascaras", 0) + mascarasColetadas);
        PlayerPrefs.SetInt("UltimasMascaras", mascarasColetadas);
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
