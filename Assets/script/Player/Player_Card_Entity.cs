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

    int origin_order;

    //각 카드 별 오더 단위 : 1 초과해야 함
    [SerializeField]
    int Order_Unit;
    //sortinglayer name
    [SerializeField]
    string Order_Name;

    //카드 원본
    //카드가 보드에 의해 정렬이 완료될 때 저장됨.
    Card_Origin card_origin;

    public void Set_card_origin()
    {
        Transform set_tempt = gameObject.transform;
        card_origin = new Card_Origin(set_tempt.position, set_tempt.rotation, set_tempt.localScale);
    }

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

    //카드의 기본 레이어 오더가 결정될 때, 기본 레이어 오더를 저장하고, 그것에 맞게 정렬하는 과정
    public void SetOriginOrder(int index)
    {
        origin_order = index * Order_Unit;
        SetOrder(origin_order);
    }


    //카드의 레이어 오더를 인자로 받아, 그 레이어 오더에 맞는 정렬을 하는 과정
    public void SetOrder(int index)
    {
        //프레임
        frame.sortingOrder = index;
        frame.sortingLayerName = Order_Name;

        //프레임 위에 올라와야 하는 이미지
        //이미지
        image.sortingLayerName = Order_Name;
        image.sortingOrder = (index + 1);
        //이름
        name_.renderer.sortingLayerName = Order_Name;
        name_.renderer.sortingOrder = (index + 1);
        //설명
        description.renderer.sortingLayerName = Order_Name;
        description.renderer.sortingOrder = (index + 1);

        //뒷면은 on/off이므로 다른 카드에 대한 순서만
        //뒷면
        Back.sortingOrder = index;
        Back.sortingLayerName = Order_Name;
        
    }


    public Vector2 GetSize()
    {
        return gameObject.GetComponent<BoxCollider2D>().size;
    }
}

