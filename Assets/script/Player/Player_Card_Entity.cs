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

    //back active 확인
    bool me = false;

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

    //카드 정보
    Player_Card data;

    //카드 잡은 여부
    bool up_down = false;
    //적이 감지된 여부
    bool enemy_on = false;
    //카드 사용 여부
    bool used = false;

    //owner
    Player owner;
    

    public void Set_card_origin()
    {
        Transform set_tempt = gameObject.transform;
        card_origin = new Card_Origin(set_tempt.position, set_tempt.rotation, set_tempt.localScale);
    }

    public void Setup(Sprite Frame_data, Sprite Image_data, string Name_data, string Description_data, Sprite Back_data, bool me, Player_Card data, Player owner)
    {

        frame.sprite = Frame_data;
        image.sprite = Image_data;
        name_.text = Name_data;
        description.text = Description_data;
        Back.sprite = Back_data;
        this.data = data;
        this.me = me;
        this.owner = owner;

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

    private void OnMouseOver()
    {
        SetOrder(500);
        transform.localScale = card_origin.origin_scale * 1.4f;
    }

    private void OnMouseExit()
    {
        SetOrder(origin_order);
        transform.localScale = card_origin.origin_scale;
    }

    private void Update()
    {
        if (!used)
        {
            if (up_down)
                Card_Drag();
            else
            {
                if (enemy_on)
                {
                    used = true;
                    Attacking();
                }
                else
                    transform.position = card_origin.origin_pos;
            }
        }

    }

    private void Card_Drag()
    {
        Vector3 position_tempt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position_tempt.x, position_tempt.y, position_tempt.z - Camera.main.transform.position.z);
    }


    //나일 때만

    private void OnMouseDown()
    {
        if(me)
            up_down = true;
    }

    private void OnMouseUp()
    {
        if(me)
            up_down = false;
    }

    Collider2D Enemy_collsion;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("Board_Enemy"))
        {
            enemy_on = true;
            Enemy_collsion = collision;
        }
        else
        {
            enemy_on = false;
            Enemy_collsion = null;
        }
    }

    void Attacking()
    {
        data.Using_Card(Enemy_collsion.GetComponent<Board_Enemy>().Enemy_deck[0]);
        SetOrder(0);
        owner.Remove_card_in_hand(gameObject);
        Enemy_collsion.GetComponent<Board_Enemy>().Using_player_card(gameObject);
        gameObject.GetComponent<Player_Card_Entity>().enabled = false;
    }
}

