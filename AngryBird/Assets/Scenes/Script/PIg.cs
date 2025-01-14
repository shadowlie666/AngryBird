using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIg : MonoBehaviour
{
    public float maxSpeed = 30;
    public float minSpeed = 4;
    private SpriteRenderer render;
    public Sprite hurt; //sprite��ͼƬ���͵ı���
    public GameObject boom;
    public GameObject score;
    public bool IsPig = false;
    public AudioClip hurtcollsion;
    public AudioClip dead;
    public AudioClip birdCollsion;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)//������ײʱ���β�������������ײ������
    {
        if(collision.gameObject.tag == "Player")
        {
            MusicPlay(birdCollsion);
            collision.transform.GetComponent<BIrd>().Hurt();
        }

        if(collision.relativeVelocity.magnitude>maxSpeed)//relativevelocity��ȡ��������ٶ�������magnitude����תΪ����
        {
            Dead();
        }
        else if(collision.relativeVelocity.magnitude>minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            MusicPlay(hurtcollsion);
            render.sprite = hurt; //��ȡ��������sprite��Ա�������Ա������ͼƬ������
        }
    }

    public void Dead()
    {
        if(IsPig)
        {
            GameManagers.instance.pIgs.Remove(this);
        }
        Destroy(gameObject);//�ݻٵ�ǰ��Ϸ����
        //����boom����ʾ�������ڵ�ǰ�����λ���ϣ����һ�������ǲ���ת��identity����Ĭ�ϵ���˼
        Instantiate(boom,transform.position, Quaternion.identity);
        GameObject sc = Instantiate(score, transform.position+new Vector3(0,0.8f,0), Quaternion.identity);
        MusicPlay(dead);
        Destroy(sc, 1.5f);
    }

    public void MusicPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
