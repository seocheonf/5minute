using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//보드가 자기 플레이어 정보 확인하고, 핸드에 있는 오브젝트를 자기에 맞춰 배치할 것임.
//레이어도, 펼치는 것도.

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

        //카드 정렬 시작

    }

}
