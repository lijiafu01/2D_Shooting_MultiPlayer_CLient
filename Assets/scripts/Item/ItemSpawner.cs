/*using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Static singleton property
    public static ItemSpawner Instance { get; private set; }

    // Initialize the singleton in the Awake function.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally: make this object persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnItem(int itemIndex,UnityEngine.Vector3 position)
    {
        // Load the prefab from the Resources folder
        GameObject itemPrefab = Resources.Load<GameObject>("texture/itemPrefabs" + itemIndex);

        // Instantiate the prefab at the specified position
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, position,UnityEngine.Quaternion.identity);
        }
        else
        {
            Debug.LogError("Could not load prefab: texture/itemPrefabs" + itemIndex);
        }
    }
}
*/