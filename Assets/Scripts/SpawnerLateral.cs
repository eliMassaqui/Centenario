using UnityEngine;

public class SpawnerLateral : MonoBehaviour
{
    public GameObject[] objetosLado;
    public float intervaloSpawn = 10f; // Distância entre os objetos
    public float distanciaSpawn = 100f; // Até onde o ambiente é gerado à frente do jogador
    public Transform jogador;

    public float posicaoY = 0f;
    public float offsetX = 5f; // Distância lateral da estrada

    private float ultimoZSpawn = 0f;

    void Update()
    {
        while (ultimoZSpawn < jogador.position.z + distanciaSpawn)
        {
            SpawnarEm(ultimoZSpawn);
            ultimoZSpawn += intervaloSpawn;
        }
    }

    void SpawnarEm(float z)
    {
        GameObject prefab = objetosLado[Random.Range(0, objetosLado.Length)];

        // Lado esquerdo
        Vector3 posEsquerda = new Vector3(-offsetX, posicaoY, z);
        Instantiate(prefab, posEsquerda, Quaternion.identity);

        // Lado direito
        Vector3 posDireita = new Vector3(offsetX, posicaoY, z);
        Instantiate(prefab, posDireita, Quaternion.identity);
    }
}
