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
            //����� ��
            switching_back_front(true);
        }
        else
        {
            //���̶�� ��
            switching_back_front(false);
        }
    }

    public void switching_back_front(bool back_or_front) //back�� false, front�� true
    {
        if(back_or_front) //���̶��
        {
            frame.enabled = true;
            image.enabled = true;
            name_.enabled = true;
            description.enabled = true;
            Back.enabled = false;
            //�� ���� ���� on
        }
        else //�ڶ��
        {
            frame.enabled = false;
            image.enabled = false;
            name_.enabled = false;
            description.enabled = false;
            Back.enabled = true;
            //�ڸ� on
        }
    }

}

