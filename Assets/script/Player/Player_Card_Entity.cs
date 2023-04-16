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

    //�� ī�� �� ���� ���� : 1 �ʰ��ؾ� ��
    [SerializeField]
    int Order_Unit;
    //sortinglayer name
    [SerializeField]
    string Order_Name;

    //ī�� ����
    //ī�尡 ���忡 ���� ������ �Ϸ�� �� �����.
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

    //ī���� �⺻ ���̾� ������ ������ ��, �⺻ ���̾� ������ �����ϰ�, �װͿ� �°� �����ϴ� ����
    public void SetOriginOrder(int index)
    {
        origin_order = index * Order_Unit;
        SetOrder(origin_order);
    }


    //ī���� ���̾� ������ ���ڷ� �޾�, �� ���̾� ������ �´� ������ �ϴ� ����
    public void SetOrder(int index)
    {
        //������
        frame.sortingOrder = index;
        frame.sortingLayerName = Order_Name;

        //������ ���� �ö�;� �ϴ� �̹���
        //�̹���
        image.sortingLayerName = Order_Name;
        image.sortingOrder = (index + 1);
        //�̸�
        name_.renderer.sortingLayerName = Order_Name;
        name_.renderer.sortingOrder = (index + 1);
        //����
        description.renderer.sortingLayerName = Order_Name;
        description.renderer.sortingOrder = (index + 1);

        //�޸��� on/off�̹Ƿ� �ٸ� ī�忡 ���� ������
        //�޸�
        Back.sortingOrder = index;
        Back.sortingLayerName = Order_Name;
        
    }


    public Vector2 GetSize()
    {
        return gameObject.GetComponent<BoxCollider2D>().size;
    }
}

