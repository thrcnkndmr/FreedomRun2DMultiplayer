using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 70f;
    [SerializeField] private Transform player; //Player nesnesi Prefab
    [SerializeField] private Transform levelPart_Start; //Start Prefab
    public Text distanceTxtCounter;
    [SerializeField] private List<Transform> levelPartList; //LevelPart1 Prefab
    [SerializeField] private List<Transform> levelPartList2;//LevelPart2 Prefab 
    [SerializeField] private List<Transform> levelPartList3;  //LevelPart3 Prefab 

    Vector3 PlayerStartPosition; // Player baþlangýç posisyonu
    Vector3 PlayerUpdatePosition; // Player güncel posisyonu

    private Vector3 lastEndPosition;
    float distance; //Son EndPosition ile player nesnesi arasýndaki mesafeyi hesaplayýp yeni zemin oluþturmak için kullanýlan deðer.
    float distance2; //Player ýn baþlangýç posisyonu ile güncel posisyonu arasýndaki mesafeyi ölçme

    int tmp = -1; //spawnlevel 1 için random kontrol
    int tmp2 = -1; //spawnlevel 2 için random kontrol
    int tmp3 = -1; //spawnlevel 3 için random kontrol

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        
        int startingSpawnLevelParts = 3; //Baþlangýçta oluþturulan zemin sayýsý
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
        PlayerStartPosition = new Vector3(player.position.x, 0f, 0f);
        
    }
    private void Update()
    {
        PlayerUpdatePosition = new Vector3(player.position.x, 0f, 0f); // Playerin güncel konumunu sürekli yaz.

        distance = Vector2.Distance(player.position,lastEndPosition);
        distance2 = Vector2.Distance(PlayerStartPosition, PlayerUpdatePosition) / 10; // Karakterin baþlangýçtan olan uzaklýðýný hesaplama
        distanceTxtCounter.text = distance2.ToString("F2") + " M"; // Ekranan karakterin gittiði mesafeyý yazdýrma

        if (distance < PLAYER_DISTANCE_SPAWN_LEVEL_PART) // Yeni prefabýn oluþturulmasý için Player ve Prefabýn end position arasý mesafeyi koþula sokma
        {
            if (distance2 < 100f) 
            {
                SpawnLevelPart(); 
            }                                            // Mesafeye uygun zorluk partýnýn oluþturulmasý için foksiyon çaðýrma
            else if (distance2 >= 100 && distance2 <200)
            {
                PlayerMovements.speed = 15f;
                SpawnLevelPart2();
            }
            else
            {
                PlayerMovements.speed = 15f;
                SpawnLevelPart3();
            }
        }
    }
    private void SpawnLevelPart() // Zorluk 1 için random platform belirlenmesi ve end position bulunmasý
    {
        
        int levelPartRandom = Random.Range(0, levelPartList.Count); 
        while(levelPartRandom == tmp)
        {
            levelPartRandom = Random.Range(0, levelPartList.Count);  
        }
        if(levelPartRandom != tmp)
        {
            Transform chosenLevelPart = levelPartList[levelPartRandom];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        }
        tmp = levelPartRandom;
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) // Zorluk 1 prefab oluþturulmasý
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
    private void SpawnLevelPart2() // Zorluk 2 için random platform belirlenmesi ve end position bulunmasý
    {
        int levelPartRandom2 = Random.Range(0, levelPartList2.Count); //3 //3

        while (levelPartRandom2 == tmp2)
        {
            levelPartRandom2 = Random.Range(0, levelPartList2.Count);

        }
        if (levelPartRandom2 != tmp2)
        {
            Transform chosenLevelPart = levelPartList2[levelPartRandom2];
            Transform lastLevelPartTransform = SpawnLevelPart2(chosenLevelPart, lastEndPosition);
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        }
        tmp2 = levelPartRandom2;

    }
    private Transform SpawnLevelPart2(Transform levelPart, Vector3 spawnPosition) // Zorluk 2 prefab oluþturulmasý
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
    private void SpawnLevelPart3() // Zorluk 3 için random platform belirlenmesi ve end position bulunmasý
    {
        int levelPartRandom3 = Random.Range(0, levelPartList3.Count);

        while (levelPartRandom3 == tmp3)
        {
            levelPartRandom3 = Random.Range(0, levelPartList3.Count);
        }
        if (levelPartRandom3 != tmp3)
        {
            Transform chosenLevelPart = levelPartList3[levelPartRandom3];
            Transform lastLevelPartTransform = SpawnLevelPart3(chosenLevelPart, lastEndPosition);
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        }
        tmp3 = levelPartRandom3;
    }
    private Transform SpawnLevelPart3(Transform levelPart, Vector3 spawnPosition) // Zorluk 3 prefab oluþturulmasý
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
