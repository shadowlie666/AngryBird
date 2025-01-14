using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIg : MonoBehaviour
{
    public float maxSpeed = 30;
    public float minSpeed = 4;
    private SpriteRenderer render;
    public Sprite hurt; //sprite是图片类型的变量
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


    private void OnCollisionEnter2D(Collision2D collision)//发生碰撞时，形参是与它发生碰撞的物体
    {
        if(collision.gameObject.tag == "Player")
        {
            MusicPlay(birdCollsion);
            collision.transform.GetComponent<BIrd>().Hurt();
        }

        if(collision.relativeVelocity.magnitude>maxSpeed)//relativevelocity获取的是相对速度向量，magnitude可以转为数字
        {
            Dead();
        }
        else if(collision.relativeVelocity.magnitude>minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            MusicPlay(hurtcollsion);
            render.sprite = hurt; //获取组件里面的sprite成员，这个成员控制着图片是哪张
        }
    }

    public void Dead()
    {
        if(IsPig)
        {
            GameManagers.instance.pIgs.Remove(this);
        }
        Destroy(gameObject);//摧毁当前游戏物体
        //生成boom所表示的物体在当前物体的位置上，最后一个参数是不旋转，identity就是默认的意思
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
