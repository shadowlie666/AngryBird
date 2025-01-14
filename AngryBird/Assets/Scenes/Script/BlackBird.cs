using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : BIrd
{
    private List<PIg> blocks = new List<PIg>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pig")
        {
            blocks.Add(collision.gameObject.GetComponent<PIg>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pig")
        {
            blocks.Remove(collision.gameObject.GetComponent<PIg>());
        }
    }

    public override void ShowSkill()
    {
        base.ShowSkill();
        if(blocks.Count>0 && blocks != null)
        {
            for(int i=0; i<blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    public void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();
    }

    protected override void NextBird()
    {
        GameManagers.instance.bIrds.Remove(this);
        Destroy(gameObject);
        GameManagers.instance.PreNextBird();
    }
}
