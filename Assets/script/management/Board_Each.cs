using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���尡 �ڱ� �÷��̾� ���� Ȯ���ϰ�, �ڵ忡 �ִ� ������Ʈ�� �ڱ⿡ ���� ��ġ�� ����.
//���̾, ��ġ�� �͵�.

public class Board_Each : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        StartCoroutine(like_update());
    }

    IEnumerator like_update()
    {
        yield return new WaitUntil(() => player != null);
        yield return new WaitUntil(() => player.card_in_hand != null);

        //ī�� ���� ����

    }

}
