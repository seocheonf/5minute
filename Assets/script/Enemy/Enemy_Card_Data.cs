using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

public class Enemy_HP
{
    public string Attack;
    public int HP_N;
}

[System.Serializable]
public class Enemy_Card_Data
{

    [SerializeField]
    private string card_name;

    public string Card_name
    {
        get { return card_name; }
    }


    [SerializeField]
    private string card_type;

    public string Card_type
    {
        get { return card_type; }
    }

    [SerializeField]
    [TextArea]
    private string card_text;

    public string Card_text
    {
        get { return card_text; }
    }

    [SerializeField]
    private Enemy_HP[] hp_inform;

    public Enemy_HP[] Hp_inform
    {
        get { return hp_inform; }
    }

    
    public Sprite Enemy_Card_Frame;

    public AssetReferenceAtlasedSprite Enemy_icon;


}