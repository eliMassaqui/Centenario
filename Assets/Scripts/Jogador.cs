using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Jogador : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelGameOver;
    public TextMeshProUGUI textoMascaras;

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
            estaVivo = false;
            MostrarGameOver();
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

    void InicializarEstado()
    {
        estaVivo = true;
        Time.timeScale = 1f;

        if (painelGameOver != null)
            painelGameOver.SetActive(false);

        AtualizarUI();
    }

    void ColetarMascara(GameObject mascara)
    {
        mascarasColetadas++;
        Destroy(mascara);
        AtualizarUI();
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
