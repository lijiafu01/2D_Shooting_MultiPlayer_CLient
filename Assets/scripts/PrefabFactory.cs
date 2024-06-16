using System.Collections.Generic;
using UnityEngine;

public class PrefabFactory : MonoBehaviour
{
    // Đây là singleton instance
    public static PrefabFactory Instance { get; private set; }

    // Đây là dictionary chứa tất cả prefabs
    //private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // Thiết lập singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void SpawnItem(int itemIndex, UnityEngine.Vector3 position)
    {
        // Load all GameObjects in the directory "Resources/texture/itemPrefabs"
        GameObject[] prefabs = Resources.LoadAll<GameObject>("texture/itemPrefabs");

        // Now you can access individual prefabs by their index in the array
        GameObject myPrefab = prefabs[itemIndex];

        // Instantiate the prefab at a specific position
        GameObject item = Instantiate(myPrefab, position, Quaternion.identity);
        item.name = myPrefab.name; // Xóa "(Clone)" khỏi tên
        SetItem(item);
        
    }
    void SetItem(GameObject item)
    {
        ItemProper itemProper = item.GetComponent<ItemProper>();
        itemProper.category = item.name;
        switch (itemProper.category)
        {
            case "hp":
                item.GetComponent<ItemProper>().proper.maxHp = 20f;
                break;
            case "damage":
                item.GetComponent<ItemProper>().proper.damage = 5f;
                break;
            case "attackSpeed":
                item.GetComponent<ItemProper>().proper.attackSpeed = 1f;
                break;
            case "crit":
                item.GetComponent<ItemProper>().proper.crit = 1f;
                break;
            case "moveSpeed":
                item.GetComponent<ItemProper>().proper.moveSpeed = 0.3f;
                break;
            case "grass":
                item.GetComponent<ItemProper>().proper.hp = 200f;
                break;
            default:
                Debug.LogError("Unknown category: " + itemProper.category);
                break;
        }


    }
    // Hàm để tải tất cả prefabs vào dictionary
    /*    private void LoadPrefabs()
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");
            foreach (GameObject prefab in prefabs)
            {
                prefabDict.Add(prefab.name, prefab);
            }
        }

        // Hàm factory để tạo mới GameObject từ prefab name
        public GameObject CreateObject(string objectName)
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{objectName}");
            if (prefab != null)
            {
                GameObject newObj = Instantiate(prefab);
                return newObj;
            }
            else
            {
                Debug.LogError($"Prefab {objectName} could not be found.");
                return null;
            }
        }*/


}
