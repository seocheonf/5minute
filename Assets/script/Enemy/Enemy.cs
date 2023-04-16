using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;


public class Enemy : MonoBehaviour
{
    public Enemy_Card_Data card_data;

    public AssetReference card_entity;

    public bool alive = true;

    private void Awake()
    {
        card_entity.InstantiateAsync(transform).Completed += handler =>
        {
            GameObject Entity = handler.Result;
            Enemy_Card_Entity Entity_inform = Entity.GetComponent<Enemy_Card_Entity>();
            card_data.Enemy_icon.LoadAssetAsync().Completed += handler2 =>
            {
                Entity_inform.Setup(card_data.Enemy_Card_Frame, handler2.Result, card_data.Card_name, card_data.Card_text);
            };
            
        };
    }

}
