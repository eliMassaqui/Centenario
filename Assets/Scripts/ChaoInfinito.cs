using UnityEngine;
using System.Collections.Generic;

public class ChaoInfinito : MonoBehaviour
{
    [Header("Blocos de Chão")]
    public GameObject[] blocosDeChao;
    public int quantidade = 5;
    public float comprimento = 30f;
    public Transform jogador;

    [Header("Obstáculos")]
    public GameObject[] obstaculos; // Prefabs dos obstáculos
    public float distanciaMinObstaculo = 10f; // Mínima distância entre obstáculos
    public float distanciaMaxObstaculo = 20f; // Máxima distância entre obstáculos
    public float posicaoYObstaculo = 0.5f; // Altura dos obstáculos no chão

    private Queue<GameObject> filaChao = new Queue<GameObject>();
    private Vector3 proximaPosicao;
    private float proximaDistanciaObstaculo;

    void Start()
    {
        proximaPosicao = Vector3.zero;
        proximaDistanciaObstaculo = Random.Range(distanciaMinObstaculo, distanciaMaxObstaculo);

        for (int i = 0; i < quantidade; i++)
        {
            CriarBloco();
        }
    }

    void Update()
    {
        GameObject primeiroBloco = filaChao.Peek();
        if (jogador.position.z - primeiroBloco.transform.position.z > comprimento)
        {
            GameObject bloco = filaChao.Dequeue();
            bloco.transform.position = proximaPosicao;
            filaChao.Enqueue(bloco);
            proximaPosicao.z += comprimento;

            // Verifica se já pode spawnar um obstáculo
            if (proximaPosicao.z >= jogador.position.z + proximaDistanciaObstaculo)
            {
                SpawnarObstaculo(proximaPosicao.z);
                proximaDistanciaObstaculo = Random.Range(distanciaMinObstaculo, distanciaMaxObstaculo);
            }
        }
    }

    void CriarBloco()
    {
        GameObject prefab = blocosDeChao[Random.Range(0, blocosDeChao.Length)];
        GameObject bloco = Instantiate(prefab, proximaPosicao, Quaternion.identity);
        filaChao.Enqueue(bloco);
        proximaPosicao.z += comprimento;
    }

    void SpawnarObstaculo(float zPos)
    {
        GameObject prefab = obstaculos[Random.Range(0, obstaculos.Length)];
        float xAleatorio = Random.Range(-2f, 2f); // Ajuste lateral conforme seu jogo
        Vector3 posicao = new Vector3(xAleatorio, posicaoYObstaculo, zPos);
        Instantiate(prefab, posicao, Quaternion.identity);
    }
}
