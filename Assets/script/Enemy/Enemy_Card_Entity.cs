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
    TextMeshPro sword;

    [SerializeField]
    TextMeshPro shield;

    [SerializeField]
    TextMeshPro jump;

    [SerializeField]
    TextMeshPro bow;

    [SerializeField]
    TextMeshPro magic;

    Dictionary<string, int> hp;

    [SerializeField]
    string Order_Name;

    public void Setup(Sprite Frame_data, Sprite Image_data, string Name_data, Dictionary<string,int> hp)
    {

        frame.sprite = Frame_data;
        image.sprite = Image_data;
        name_.text = Name_data;
        this.hp = hp;

        StartCoroutine(hp_update());

        SetOrder();

    }

    IEnumerator hp_update()
    {
        while (true)
        {
            sword.text = "Sword : " + hp["Sword"];
            shield.text = "Shield : " + hp["Shield"];
            jump.text = "Jump : " + hp["Jump"];
            magic.text = "Magic : " + hp["Magic"];
            bow.text = "Bow : " + hp["Bow"];

            yield return null;
        }
    }

    //ī���� ���̾� ������ ���ڷ� �޾�, �� ���̾� ������ �´� ������ �ϴ� ����
    public void SetOrder()
    {
        //������
        frame.sortingOrder = 0;
        frame.sortingLayerName = Order_Name;

        //������ ���� �ö�;� �ϴ� �̹���
        //�̹���
        image.sortingLayerName = Order_Name;
        image.sortingOrder = 1;
        //�̸�
        name_.renderer.sortingLayerName = Order_Name;
        name_.renderer.sortingOrder = 1;
        //����
        sword.renderer.sortingLayerName = Order_Name;
        sword.renderer.sortingOrder = 1;

        shield.renderer.sortingLayerName = Order_Name;
        shield.renderer.sortingOrder = 1;

        jump.renderer.sortingLayerName = Order_Name;
        jump.renderer.sortingOrder = 1;

        magic.renderer.sortingLayerName = Order_Name;
        magic.renderer.sortingOrder = 1;

        bow.renderer.sortingLayerName = Order_Name;
        bow.renderer.sortingOrder = 1;

    }
}
