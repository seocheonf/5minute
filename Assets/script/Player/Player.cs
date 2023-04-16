using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;

public class Player : MonoBehaviour
{
    public bool me;

    public List<string> card_in_deck;

    public Card_N[] player_type;

    public Player_Type typetest;

    //���� �����͵��� ��Ʈ��ũ�� ���� �޾ƿ;� �� �ٸ� �������� ����.

    //�÷��̾��� ������ ������ ���۵Ǹ�, Player_Management���� ���� ������� table�� ��������.


    public List<GameObject> card_in_hand;


    public List<string> card_in_sacrifice;

    

    //ī�� ��ƼƼ ����

    public AssetReference Player_Card_Entity_inform;


    //player management�� ���� �޴� ī�忡 ���� ��ī�̺�
    public Dictionary<string, GameObject> All_Card_Information;
    public Sprite Card_Back;


    //Board_Each ����

    public Board_Each Player_Board;

    private void Awake()//�÷��̾� ī�� Ÿ�� ���� �׽�Ʈ
    {
        if (gameObject.name.Equals("Player"))
        {
            player_type = typetest.Green;
            me = true;
        }
        else
        {
            player_type = typetest.Red;
            me = false;
        }
    }

    private void Start()
    {
        StartCoroutine(drawcard());
    }

    IEnumerator drawcard()
    {
        yield return new WaitUntil(()=>All_Card_Information != null && Card_Back != null); //ī�� ����(�̹���, �ؽ�Ʈ ��)�� �غ�Ǳ� ������ ��ο����� �ʴ´�. //�� ������ �Ϸ�Ǳ� �������� ��ο����� �ʴ´�. ---- //��ü ĳ���� ������ ��ȸ�Ͽ�, �ʿ��� ī�������� �����Ǳ� �������� ī�� ��ο츦 �������� �ʴ´� -> All_Card_Information != null

        bool wait = false;

        while (true)
        {
            if (card_in_hand.Count < 4 && card_in_deck.Count > 0)
            {
                wait = true;
                Player_Card_Entity_inform.InstantiateAsync(transform).Completed += handler =>
                {
                    GameObject Entity = handler.Result;
                    StartCoroutine(card_generation(Entity.GetComponent<Player_Card_Entity>(), All_Card_Information[card_in_deck[0]].GetComponent<Player_Card>().card_data, All_Card_Information[card_in_deck[0]].GetComponent<Player_Card>(), gameObject.GetComponent<Player>())); //������ ��ƼƼ ������ ī�� ������ ã�� ���Խ�Ŵ
                    Add_card_in_hand(Entity); //������ ��ƼƼ ������ �ڵ忡 ����
                    card_in_deck.RemoveAt(0); //���� �� ���� ������
                    wait = false;
                };

            }
            yield return new WaitUntil(() => wait == false); //���� üũ ���ϰ� ����ó�� �����긦 �ۿ����ϸ� ������ �����ǰ�, handler���� ó���� �Ǽ� index �ƿ� ������ �߰Ե�. ������ �۾��� ��ģ �Ŀ� ���� �۾��� �����ϵ��� ������ ��.
        }
    }

    //ī�� ��ü ���� ����
    IEnumerator card_generation(Player_Card_Entity A, Player_Card_Data B, Player_Card C, Player owner)
    {
        while (B.card_image_sprite == null || B.card_frame_sprite == null)
        {
            yield return null;
        }
        //Entity�� �� �ڽĿ� ī�� �����͸� ������ ���� ��Ŵ.
        A.Setup(B.card_frame_sprite, B.card_image_sprite, B.Card_name, B.Card_text, Card_Back, me, C, owner);

    }

    //�ڵ忡 ī�� �߰�.
    public void Add_card_in_hand(GameObject Entity)
    {
        card_in_hand.Add(Entity);
        //ī�尡 �߰� �� �� �ڵ� ������ ���忡 �Ѱ���
        Player_Board.SpreadCard(card_in_hand);
    }

    //�ڵ忡 ī�� ����
    public void Remove_card_in_hand(GameObject Entity)
    {
        /* 
         * ������ Player_Card_Entity���� �ι��� �ҷ��� ������ ���ܹ���. �ϴ� ���� üũ �� �־�δµ�, �� ���̽��� ����� ������ ����� �� ��.
         */
        int index = card_in_hand.IndexOf(Entity);

        if (index < 0) //ã�� ���� �� �����̹Ƿ� ���� ó��. -1�� ��� ��ã�� ��.
            return;

        card_in_hand.RemoveAt(index);
        //ī�尡 ���� �� �� �ڵ� ������ ���忡 �Ѱ���
        Player_Board.SpreadCard(card_in_hand);
    }

    


}
