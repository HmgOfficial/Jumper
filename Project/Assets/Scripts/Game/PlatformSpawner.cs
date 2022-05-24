using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance;

    [Header("Prefab")]
    public GameObject[] PlatformPrefabs;

    [Header("Container")]
    public GameObject PlatformPoolContainer;
    public GameObject PlatformInGameContainer;

    private List<Platform> platformsPool;
    private List<Platform> platformsInGame;

    private void Awake()
    {
        Instance = this;

        platformsPool = new List<Platform>();
        platformsInGame = new List<Platform>();
    }

    void Start()
    {
        StartCoroutine(PoolSpawn());
    }

    /// <summary>
    /// Initial platforms
    /// </summary>
    /// <returns></returns>
    IEnumerator InitialSpawn()
    {
        for (int i = 0; i < 5; i++)
        {
            // TODO   RANDOM PLATFORM TYPE
            Spawn();
            yield return 0;
        }
    }

    /// <summary>
    /// Create the platform pool
    /// </summary>
    /// <returns></returns>
    private IEnumerator PoolSpawn()
    {
        //Local variables
        GameObject newPlatform;

        for (int i = 0; i < PlatformPrefabs.Length; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                // Platform create
                newPlatform = Instantiate(PlatformPrefabs[i], PlatformPoolContainer.transform);
                newPlatform.name = "platform" + newPlatform.GetComponent<Platform>().platformType;
                newPlatform.SetActive(false);
                // Add to the pool list
                platformsPool.Add(newPlatform.GetComponent<Platform>());
                yield return 0;
            }
        }
        ProjectTools.Shuffle<Platform>(platformsPool);
        StartCoroutine(InitialSpawn());
    }

    /// <summary>
    /// Ask for a platform and if it's null make the platform
    /// </summary>
    /// <param name="pt"></param>
    public void Spawn()
    {
        GameObject newPlatform = GetPlatform(PlatformType.Basic);
        if (newPlatform == null)
        {
            newPlatform = Instantiate(PlatformPrefabs[0], this.transform);
        }
        platformsInGame.Add(newPlatform.GetComponent<Platform>());
        PlacePlatform(newPlatform);
    }

    /// <summary>
    /// Returns the platform with the desired type or a null 
    /// </summary>
    public GameObject GetPlatform(PlatformType pt)
    {
        foreach (Platform p in platformsPool)
        {
            if (p.platformType == pt)
            {
                platformsPool.Remove(p);
                return p.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Place the platform
    /// </summary>
    /// <param name="go"></param>
    private void PlacePlatform(GameObject go)
    {
        go.transform.SetParent(PlatformInGameContainer.transform);

        go.transform.localPosition = new Vector3(Random.Range(-4f, 4f), platformsInGame[platformsInGame.Count - 1].GetComponent<Platform>().nextPlatformPos.y, 0);
        go.SetActive(true);
    }

    /// <summary>
    /// Move the platform to the pool
    /// </summary>
    /// <param name="p"></param>
    public void RemovePlatform(Platform p = null)
    {
        if (p == null)
        {
            p = platformsInGame[0];
        }
        //TESTING
        else
        {
            print("La plataform a remover no es null " + p.platformType);
        }
        //Removes the platform
        p.gameObject.SetActive(false);
        platformsPool.Add(p);
        platformsInGame.Remove(p);
        //Spawns other platform
        Spawn();
    }
}
