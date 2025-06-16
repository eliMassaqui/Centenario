using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float velocidade = 10f;
    public float lateralSpeed = 5f;
    public float limiteX = 3f;

    void Update()
    {
        // Corre sempre pra frente
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);

        // Movimento lateral (setas ou A/D)
        float inputX = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position + Vector3.right * inputX * lateralSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -limiteX, limiteX);
        transform.position = pos;
    }
}
