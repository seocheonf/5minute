using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Enemy : MonoBehaviour
{
    public List<GameObject> Enemy_deck;

    public List<GameObject> Player_deck;

    int enemy_position = 3; //enemy card À§Ä¡

    public int enemy_count; //enemy count ¼ö

    private void Start()
    {
        Player_deck = new List<GameObject>();
        StartCoroutine(like_update());
    }

    IEnumerator like_update()
    {
        yield return new WaitUntil(() => Enemy_deck.Count == enemy_count);


        while(Enemy_deck.Count > 0)
        {
            GameObject Enemy_tempt = Enemy_deck[0];

            Enemy_tempt.transform.position = new Vector3(transform.position.x + enemy_position, transform.position.y, transform.position.z);

            yield return new WaitUntil(()=> Enemy_tempt.GetComponent<Enemy>().alive == false);
            
            Enemy_deck.RemoveAt(0);
            Destroy(Enemy_tempt);
        }


        Debug.Log("complete!");
    }

    private void Update()
    {
        if(Player_deck.Count > 0)
        {
            for(int i = 0; i<Player_deck.Count - 1; i++)
            {
                Player_deck[i].SetActive(false);
            }

            Player_deck[Player_deck.Count - 1].SetActive(true);

        }
    }

    public void Using_player_card(GameObject player_card)
    {
        if (Player_deck.Count > 0)
            Player_deck[Player_deck.Count - 1].SetActive(false);

        Player_deck.Add(player_card);
        player_card.transform.position = new Vector3(transform.position.x - enemy_position, transform.position.y, transform.position.z);
    }

}
