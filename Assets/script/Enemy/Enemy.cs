using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;


public class Enemy : MonoBehaviour
{
    public Enemy_Card_Data card_data;

    public AssetReference card_entity;

    public bool alive = true;

    //체력 정보 탐색
    public Dictionary<string, int> hp;
    //남은 전체 체력 정보
    public int hp_all = 0;

    private void Awake()
    {
        hp = new Dictionary<string, int>();
        foreach(Enemy_HP each_hp in card_data.Hp_inform)
        {
            hp.Add(each_hp.Attack, each_hp.HP_N);
            hp_all += each_hp.HP_N;
        }

        card_entity.InstantiateAsync(transform).Completed += handler =>
        {
            GameObject Entity = handler.Result;
            Enemy_Card_Entity Entity_inform = Entity.GetComponent<Enemy_Card_Entity>();
            card_data.Enemy_icon.LoadAssetAsync().Completed += handler2 =>
            {
                Entity_inform.Setup(card_data.Enemy_Card_Frame, handler2.Result, card_data.Card_name, hp);
            };
            
        };
    }

}
