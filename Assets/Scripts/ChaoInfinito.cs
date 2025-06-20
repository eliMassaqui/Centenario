using UnityEngine;
using System.Collections.Generic;

public class ChaoInfinito : MonoBehaviour
{
    public GameObject[] blocosDeChao; // Prefabs dos blocos
    public int quantidade = 4; // Quantos blocos em cena
    public float comprimento = 30f; // Comprimento de cada bloco
    public Transform jogador;

    private Queue<GameObject> filaChao = new Queue<GameObject>();
    private Vector3 proximaPosicao;

    void Start()
    {
        proximaPosicao = Vector3.zero;

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
            // Reposiciona o primeiro bloco no final
            GameObject bloco = filaChao.Dequeue();
            bloco.transform.position = proximaPosicao;
            filaChao.Enqueue(bloco);
            proximaPosicao.z += comprimento;
        }
    }

    void CriarBloco()
    {
        GameObject prefab = blocosDeChao[Random.Range(0, blocosDeChao.Length)];
        GameObject bloco = Instantiate(prefab, proximaPosicao, Quaternion.identity);
        filaChao.Enqueue(bloco);
        proximaPosicao.z += comprimento;
    }
}
