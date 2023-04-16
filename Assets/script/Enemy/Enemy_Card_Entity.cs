using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Enemy_Card_Entity : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer frame;

    [SerializeField]
    SpriteRenderer image;

    [SerializeField]
    TextMeshPro name_;

    [SerializeField]
    TextMeshPro description;


    public void Setup(Sprite Frame_data, Sprite Image_data, string Name_data, string Description_data)
    {

        frame.sprite = Frame_data;
        image.sprite = Image_data;
        name_.text = Name_data;
        description.text = Description_data;

    }
}
