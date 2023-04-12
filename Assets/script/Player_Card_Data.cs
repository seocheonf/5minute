using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

[System.Serializable]
public class Player_Card_Data
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

    public Sprite card_frame_sprite;
    public Sprite card_image_sprite;

    public AssetReferenceAtlasedSprite card_frame_sprite_atlas;
    public AssetReferenceAtlasedSprite card_image_sprite_atlas;
}