using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Jogador : MonoBehaviour
{
    public float velocidade = 10f;
    public float lateralSpeed = 5f;
    public float limiteX = 3f;

    [Header("UI Game Over")]
    public GameObject painelGameOver;

    [Header("UI Recompensa")]
    public TextMeshProUGUI textoMascaras; // arrasta o texto no inspector

    private int mascarasColetadas = 0;
    private bool estaVivo = true;

    void Start()
    {
        estaVivo = true;
        Time.timeScale = 1f;

        if (painelGameOver != null)
            painelGameOver.SetActive(false);

        if (textoMascaras != null)
            textoMascaras.text = "Máscaras: 0";
    }

    void Update()
    {
        if (!estaVivo) return;

        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);

        float inputX = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position + Vector3.right * inputX * lateralSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -limiteX, limiteX);
        transform.position = pos;
    }

    void OnCollisionEnter(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Obstaculo"))
        {
            estaVivo = false;
            MostrarGameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mascara"))
        {
            mascarasColetadas++;
            Destroy(other.gameObject);

            if (textoMascaras != null)
                textoMascaras.text = "Mascaras: " + mascarasColetadas;
        }
    }

    void MostrarGameOver()
    {
        Time.timeScale = 0f;
        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        // Salvar quantidade de máscaras
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
