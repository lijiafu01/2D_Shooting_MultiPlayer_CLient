using GameClient.Constructor;
using GameClient.Enums;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : MonoBehaviour
{
    public int enemyID;
    public Enemy _enemy;
    public Slider healthSlider;
    private float maxHealth;
    public float currentHealth;
    public Text nameText;
    public float angle = 180;
    public UnityEngine.Vector3 target;
    public float rotaSpeed = 1f;
    private SpriteRenderer spriteRenderer;
    public float speed = 3.0f; 

    private Rigidbody2D rb;
    public Enemy enemy
    {
        get { return _enemy; }
        set
        {
            _enemy = value;
        }
    }
    public void FireBullets(int bulletCount, float timeBetweenBullets)
    {
        StartCoroutine(FireBulletsCoroutine(bulletCount, timeBetweenBullets));
    }

    private IEnumerator FireBulletsCoroutine(int bulletCount, float timeBetweenBullets)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // Create a bullet
            GameObject bullet = Instantiate(GamePlayManager.Instance.enemyBullet, transform.GetChild(5).transform.position, transform.GetChild(5).transform.rotation);
            bullet.GetComponent<EnemyBullet>().damage = enemy.enemyProper.damage;

            // Wait for the specified time
            yield return new WaitForSeconds(timeBetweenBullets);
        }
    }


private void Start()
    {
        speed = enemy.enemyProper.moveSpeed;
        enemyID = enemy.enemyID;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        maxHealth = 500f;
        currentHealth = enemy.enemyProper.hp;
        //nameText.text = enemy.name;
        nameText.text = "";
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        transform.rotation = UnityEngine.Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, enemy.quaternion.z);

        FireBullets(5, 1f);
    }


    public void TakeDamage(float amount,bool isCritDamage = false)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        enemy.enemyProper.hp = currentHealth;
        //if (currentHealth <= 0) return;
        GameObject damageText = Instantiate(GamePlayManager.Instance.damageTextPrefab, transform.position, UnityEngine.Quaternion.identity, GamePlayManager.Instance.damageTextParent);
        damageText.GetComponent<Text>().text = amount.ToString();
        if (isCritDamage)
        {
            
            damageText.GetComponent<Text>().fontSize = 25; // Chỉnh size của text
            damageText.GetComponent<Text>().color = Color.yellow; // Chỉnh màu của text
        }
        else
        {          
            damageText.GetComponent<Text>().fontSize = 15; // Chỉnh size của text
            damageText.GetComponent<Text>().color = Color.red; // Chỉnh màu của text
        }
    }
    public void Die()
    {
        if(UnityEngine.Random.Range(1, 11) <= 5)
        {
            SendSpawnItem();
        }
        SendSpawEnemy();
    }
    void SendSpawnItem()
    {
        int itemIndex = UnityEngine.Random.Range(0, 6);
        GameClient.Constructor.Vector3 vector3 = new GameClient.Constructor.Vector3(transform.position.x, transform.position.y, 0f);
        var data2 = new Dictionary<byte, object>();

        data2[1] = GamePlayCode.SpawnItem;
        data2[2] = itemIndex;
        data2[3] = JsonConvert.SerializeObject(vector3);
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data2, true);
    }
    void SendSpawEnemy()
    {
        float x = UnityEngine.Random.Range(-25f, 25f);
        float y = UnityEngine.Random.Range(-25f, 25f);
        float zAngle = UnityEngine.Random.Range(0f, 360f);
        GameClient.Constructor.Vector3 vector3 = new GameClient.Constructor.Vector3(x, y, 0f);
        UnityEngine.Vector3 newPos = new UnityEngine.Vector3(x, y, 0);
        int newCategory = UnityEngine.Random.Range(0, 8);
        //SetNewEnemy(newPos, newCategory,zAngle);

        var data2 = new Dictionary<byte, object>();

        data2[1] = GamePlayCode.EnemyDeath;
        data2[2] = enemy.enemyID;
        data2[3] = JsonConvert.SerializeObject(vector3);
        data2[4] = newCategory;
        data2[5] = zAngle;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data2, true);
    }
    public void SetNewEnemy(UnityEngine.Vector3 newPos,int spriteIndex,float angle)
    {
        enemy.enemyProper.hp = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        transform.position = newPos;
        //UnityEngine.Quaternion quaternion = new UnityEngine.Quaternion(0,0,angle,1f);
        transform.rotation = UnityEngine.Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);

        enemy.category = spriteIndex;
        Sprite[] sprites = Resources.LoadAll<Sprite>("texture/enemy");
        if (sprites.Length > 0)
        {
            Sprite sprite = sprites[spriteIndex];
            spriteRenderer.sprite = sprite;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            BulletController bl = other.GetComponent<BulletController>();
            if (!bl.isShooter) return;
            float damage = bl.damage;
            

            var data = new Dictionary<byte, object>();
            data[1] = GamePlayCode.EnemyGetDame;
            data[2] = enemy.enemyID;
            data[3] = damage;
            data[4] = bl.shooterID;
            data[5] = bl.isCrit;
            PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data, true);
            /* var enemy1 = GamePlayManager.Instance.enemyList.Find(x => x.enemyID == enemyID);
             enemy1.enemyProper.hp -= damage;*/

            float checkHp = currentHealth - damage;
            
            Destroy(other.gameObject);

        }
        
    }
    /*private void FixedUpdate()
    {
        if (GamePlayManager.Instance == null)
        {
            Debug.Log("GamePlayManager.Instance is null");
        }
        else if (GamePlayManager.Instance.enemyDict == null)
        {
            Debug.Log("GamePlayManager.Instance.enemyDict is null");
        }
        else
        {
            if (GamePlayManager.Instance.playerDict.Count == 0) return;  // Return early if dictionary is empty

            var lastItem = GamePlayManager.Instance.playerDict.Last();

            if (lastItem.Key != UserManager.userData.userID) return;
            //rb.rotation += rotaSpeed;
            transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, UnityEngine.Quaternion.Euler(0, 0, angle), rotaSpeed * Time.deltaTime);
        }
    }*/


    private void Update()
    {
        //if (UserManager.userData.isOwner) return;
        if (GamePlayManager.Instance == null)
        {
            return;
        }
        else if (GamePlayManager.Instance.enemyDict[enemyID] == null)
        {
            Debug.Log("enemy ko co trong dict  aaaaaaa");
        }
        else
        {
            
            if (transform.position != target)
            {
                transform.position = UnityEngine.Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }

    }





}
