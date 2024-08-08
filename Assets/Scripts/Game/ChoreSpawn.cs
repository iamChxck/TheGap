using System.Collections.Generic;
using UnityEngine;

public class ChoreSpawn : MonoBehaviour
{

    public List<Transform> spawnLoc = new List<Transform>();
    public List<GameObject> chorePrefab = new List<GameObject>();
    public List<GameObject> spawnedChores = new List<GameObject>();
    public int randChore;

    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject choreSpawnLoc in GameObject.FindGameObjectsWithTag("ChoreSpawnLoc"))
        {
            spawnLoc.Add(choreSpawnLoc.transform);
        }

        InstantiateChores();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateChores()
    {
        int amountToSpawn = Random.Range(1, 4);
        do
        {
            randChore = Random.Range(0, 3);
            if (chorePrefab[randChore] != spawnedChores.Contains(chorePrefab[randChore]))
            {
                spawnedChores.Add(chorePrefab[randChore]);
                Instantiate(chorePrefab[randChore], spawnLoc[randChore].position, Quaternion.identity);

            }
        } while (spawnedChores.Count != amountToSpawn);

    }
}
