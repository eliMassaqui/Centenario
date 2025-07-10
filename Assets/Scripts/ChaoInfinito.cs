using UnityEngine;
using System.Collections.Generic;

public class ChaoInfinito : MonoBehaviour
{
    public GameObject[] blocosNaCena; // Blocos já posicionados na cena
    public float comprimento = 30f;   // Comprimento de cada bloco
    public float velocidade = 10f;    // Velocidade do movimento do chão

    private Queue<GameObject> filaChao = new Queue<GameObject>();

    void Start()
    {
        Vector3 posicaoInicial = Vector3.zero;

        foreach (GameObject bloco in blocosNaCena)
        {
            bloco.transform.position = posicaoInicial;
            bloco.SetActive(true);
            filaChao.Enqueue(bloco);
            posicaoInicial.z += comprimento;
        }
    }

    void Update()
    {
        float deslocamento = velocidade * Time.deltaTime;

        foreach (GameObject bloco in filaChao)
        {
            bloco.transform.position -= new Vector3(0f, 0f, deslocamento);
        }

        if (filaChao.Count == 0) return;

        GameObject primeiroBloco = filaChao.Peek();
        if (primeiroBloco.transform.position.z < -comprimento)
        {
            GameObject bloco = filaChao.Dequeue();
            GameObject ultimoBloco = GetUltimoBloco();
            bloco.transform.position = ultimoBloco.transform.position + new Vector3(0f, 0f, comprimento);
            filaChao.Enqueue(bloco);
        }
    }

    GameObject GetUltimoBloco()
    {
        GameObject[] array = filaChao.ToArray();
        return array[array.Length - 1];
    }
}
