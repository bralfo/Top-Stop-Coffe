using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;

    public float spawnTime = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnNPC), 1f, spawnTime);
    }

    void SpawnNPC()
    {
        Instantiate(npcPrefab, transform.position, Quaternion.identity);
    }
}