using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour
{
    public float velocidade = 10f;
    public float lateralSpeed = 5f;
    public float limiteX = 3f;

    [Header("UI Game Over")]
    public GameObject painelGameOver;

    private bool estaVivo = true;

    void Start()
    {
        estaVivo = true;
        Time.timeScale = 1f;

        if (painelGameOver != null)
            painelGameOver.SetActive(false);
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
        SceneManager.LoadScene("MENU"); // Ajusta o nome da tua cena de menu
    }
}
