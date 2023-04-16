using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class Enemy_Management : MonoBehaviour
{
    [SerializeField]
    AssetReferenceAtlasedSprite Card_Frame_Reference; //적 카드 프레임 정보

    public Sprite Enemy_Card_Frame;


    //적 카드 정보 어드레서블 참조
    string enemy_card_AssetReference_path = "Assets/Prefab/Enemy_card/";

    //카드 엔티티 정보
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
        board_management.Board_Generation_Enemy(Enemy_deck); //Enemy 정보를 넘겨, 각각에 맞는 보드 판 생성

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

    //Enemy카드 frame은 공유 이미지므로, 이것만 반복해서 사용.
    IEnumerator card_generation(Enemy A)
    {
        yield return new WaitUntil(() => Enemy_Card_Frame != null);

        A.card_data.Enemy_Card_Frame = Enemy_Card_Frame;

    }
}
