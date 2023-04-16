using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Enemy : MonoBehaviour
{
    public List<GameObject> Enemy_deck;


    private void Start()
    {
        StartCoroutine(like_update());
    }

    IEnumerator like_update()
    {
        yield return new WaitUntil(() => Enemy_deck.Count != 0);


        while(Enemy_deck.Count > 0)
        {
            GameObject Enemy_tempt = Enemy_deck[0];

            Enemy_tempt.transform.position = transform.position;

            yield return new WaitUntil(()=> Enemy_tempt.GetComponent<Enemy>().alive == false);
            
            Enemy_deck.RemoveAt(0);
            Destroy(Enemy_tempt);
        }

        Debug.Log("complete!");
    }
        
    
}
