using UnityEngine;
using System.Collections.Generic;

public class CameraSeguir : MonoBehaviour
{
    public Transform jogador;    // arrasta o objeto "Player" aqui
    public Vector3 offset = new Vector3(0, 5f, -10f); // posição da câmera em relação ao jogador
    public float suavidade = 5f; // quanto mais alto, mais suave

    void LateUpdate()
    {
        if (jogador == null) return;

        Vector3 destino = jogador.position + offset;
        transform.position = Vector3.Lerp(transform.position, destino, Time.deltaTime * suavidade);
    }
}
