using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    [Header("Vida")]
    public Image barraVida;
    public float vida = 100f;
    private float danoPorColisao = 100f / 15f;
    private bool estaVivo = true;

    [Header("UI")]
    public GameObject painelGameOver;

    [Header("Imagem de Dano")]
    public Image imagemDano;                  // Imagem full screen na tela
    public Color corDano = new Color(1f, 0f, 1f, 0.5f); // Editável no Inspector

    void Start()
    {
        estaVivo = true;
        Time.timeScale = 1f;

        if (painelGameOver != null)
            painelGameOver.SetActive(false);

        if (imagemDano != null)
            imagemDano.color = new Color(corDano.r, corDano.g, corDano.b, 0f); // Invisível no início

        AtualizarBarraVida();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!estaVivo) return;

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            vida -= danoPorColisao;
            AtualizarBarraVida();

            if (imagemDano != null)
                StartCoroutine(MostrarDano());

            if (vida <= 0f)
            {
                estaVivo = false;
                MostrarGameOver();
            }
        }
    }

    void AtualizarBarraVida()
    {
        if (barraVida != null)
            barraVida.fillAmount = vida / 100f;
    }

    System.Collections.IEnumerator MostrarDano()
    {
        imagemDano.color = corDano; // Aparece com opacidade definida
        yield return new WaitForSeconds(0.2f);
        imagemDano.color = new Color(corDano.r, corDano.g, corDano.b, 0f); // Some
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
