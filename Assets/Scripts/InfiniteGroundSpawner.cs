using UnityEngine;
using System.Collections.Generic;

public class InfiniteGroundSpawner : MonoBehaviour
{
    public List<GameObject> groundPrefabs; // Lista de blocos diferentes.
    public int poolSize = 10; // Quantos blocos no total.
    public float groundLength = 10f; // Comprimento de cada bloco
    public Transform player; // Referência ao jogador
    public float spawnOffset = 30f; // Distância à frente do jogador para spawnar

    private List<GameObject> groundPool;
    private float nextSpawnZ = 0f;
    private int lastUsedIndex = -1;

    void Start()
    {
        groundPool = new List<GameObject>();

        // Inicializa o pool com blocos aleatórios
        for (int i = 0; i < poolSize; i++)
        {
            SpawnInitialBlock();
        }
    }

    void Update()
    {
        // Quando o jogador se aproxima do fim do chão atual
        if (player.position.z + spawnOffset > nextSpawnZ)
        {
            RecycleGround();
        }
    }

    void SpawnInitialBlock()
    {
        GameObject prefab = GetRandomGround();
        GameObject obj = Instantiate(prefab, Vector3.forward * nextSpawnZ, Quaternion.identity);
        obj.transform.SetParent(transform); // Organiza na Hierarquia
        groundPool.Add(obj);
        nextSpawnZ += groundLength;
    }

    void RecycleGround()
    {
        GameObject obj = groundPool[0];
        groundPool.RemoveAt(0);

        // Atualiza posição e aparência
        obj.transform.position = Vector3.forward * nextSpawnZ;

        // OPCIONAL: mudar cor para variar visualmente
        RandomizeColor(obj);

        groundPool.Add(obj);
        nextSpawnZ += groundLength;
    }

    GameObject GetRandomGround()
    {
        int index = Random.Range(0, groundPrefabs.Count);

        // Evita repetir o mesmo bloco duas vezes seguidas
        while (index == lastUsedIndex && groundPrefabs.Count > 1)
        {
            index = Random.Range(0, groundPrefabs.Count);
        }

        lastUsedIndex = index;
        return groundPrefabs[index];
    }

    void RandomizeColor(GameObject ground)
    {
        Renderer rend = ground.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
