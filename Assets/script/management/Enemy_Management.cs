using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class Enemy_Management : MonoBehaviour
{
    [SerializeField]
    AssetReferenceAtlasedSprite Card_Frame_Reference; //�� ī�� ������ ����

    public Sprite Enemy_Card_Frame;


    //�� ī�� ���� ��巹���� ����
    string enemy_card_AssetReference_path = "Assets/Prefab/Enemy_card/";

    //ī�� ��ƼƼ ����
    public AssetReference Enemy_Card_Entity_inform;


    public Enemy_Type typetest;

    public Enemy_Card_N[] enemy_type;


    List<GameObject> Enemy_deck;

    private void Awake()
    {
        Card_Frame_Reference.LoadAssetAsync().Completed += handler =>
        {
            Enemy_Card_Frame = handler.Result;
        };

        Enemy_deck = new List<GameObject>();
    }

    private void Start()
    {
        enemy_type = typetest.A;
        enemy_generation();
    }

    public void enemy_generation()
    {
        Board_Management board_management = GameObject.Find("Board_Management").GetComponent<Board_Management>();
        board_management.Board_Generation_Enemy(Enemy_deck); //Enemy ������ �Ѱ�, ������ �´� ���� �� ����

        foreach (Enemy_Card_N each_enemy in enemy_type)
        {
            Addressables.InstantiateAsync(enemy_card_AssetReference_path + each_enemy.Get_enemy_card_name + ".prefab").Completed += handler =>
            {
                GameObject Entity = handler.Result;
                Entity.transform.position = new Vector3(Entity.transform.position.x + 100, Entity.transform.position.y, Entity.transform.position.z);
                StartCoroutine(card_generation(Entity.GetComponent<Enemy>()));
                Enemy_deck.Add(Entity);
                Shuffle.Shuffle_Run(Enemy_deck);
            };
        }
    }

    //Enemyī�� frame�� ���� �̹����Ƿ�, �̰͸� �ݺ��ؼ� ���.
    IEnumerator card_generation(Enemy A)
    {
        yield return new WaitUntil(() => Enemy_Card_Frame != null);

        A.card_data.Enemy_Card_Frame = Enemy_Card_Frame;

    }
}
