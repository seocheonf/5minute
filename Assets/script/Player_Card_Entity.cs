using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Player_Card_Entity : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer frame;

    [SerializeField]
    SpriteRenderer image;

    [SerializeField]
    TextMeshPro name_;

    [SerializeField]
    TextMeshPro description;

    [SerializeField]
    SpriteRenderer Back;

    public void Setup(Sprite Frame_data, Sprite Image_data, string Name_data, string Description_data, Sprite Back_data, bool me)
    {
        frame.sprite = Frame_data;
        image.sprite = Image_data;
        name_.text = Name_data;
        description.text = Description_data;
        Back.sprite = Back_data;

        if (me)
        {
            //나라면 앞
            switching_back_front(true);
        }
        else
        {
            //남이라면 뒤
            switching_back_front(false);
        }
    }

    public void switching_back_front(bool back_or_front) //back이 false, front가 true
    {
        if(back_or_front) //앞이라면
        {
            frame.enabled = true;
            image.enabled = true;
            name_.enabled = true;
            description.enabled = true;
            Back.enabled = false;
            //뒤 빼고 전부 on
        }
        else //뒤라면
        {
            frame.enabled = false;
            image.enabled = false;
            name_.enabled = false;
            description.enabled = false;
            Back.enabled = true;
            //뒤만 on
        }
    }

}

