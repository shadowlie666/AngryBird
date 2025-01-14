using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIrd : MonoBehaviour
{
    private bool isClick;
    public Transform rightPos;
    public Transform leftPos;
    public float maxDis = 3;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;
    public LineRenderer right; //һ�����ߵ����
    public LineRenderer left;
    public GameObject boom;
    protected TestMyTrail myTrail;
    private bool canMove = true;
    public float smooth = 3;
    public AudioClip select;
    public AudioClip fly;
    public bool isFly = false;
    public Sprite hurt;
    protected SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)//������һֱ����
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//��С��λ�������λ��ͬ��
            transform.position += new Vector3(0, 0, 10);//2d��ϷҪ����z���ͬ��

            if(Vector3.Distance(transform.position,rightPos.position)>maxDis)//����С���뵯��֮�������������
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//��λ��������С��֮�������������ȡ����
                pos *= maxDis;//�������λ�������賤�ȣ�����������������ϵ����������󳤶�
                transform.position = rightPos.position + pos;//����С���ڵ�ǰ������ֻ�������λ�ô��ţ���������������
            }
            Line();
        }

        //�������
        //��ʵ�ַ�ʽ�����������Բ�ֵ�����������x������С���x��������ƽ���ƶ������������ƶ���ȥ������Ҫʹ�����Բ�ֵ����ƽ���ƶ���
        //���е�clamp�������������ģ���Ϊ����С��ȥ����������ţ���С��ɵ�Զ������Ͳ����ˣ�����˼�������x��0-15��ôx��������ֵ��������������������ôx����0����15
        //time.deltatime��һ�룬��Ϊ��Ϸ���е�֡����仯������һ֡��ʱ�������1/30����1/60֮��ģ�time.deltatime=һ֡������ * һ���֡��=һ�룬����������Ϸ����֡��һ����һ��
        float birdX = this.transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(birdX, 1.06f, 12), Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);
    
        if(isFly)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    private void Awake()//ͨ��awake��������Щ�������ʵ�������������Ǿͱ�ʾ��ǰ�����������
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()//��갴��ʱ
    {
        if(canMove)
        {
            MusicPlay(select);
            isClick = true;
            rg.isKinematic = true;//������ģ���л�Ϊ����ѧģʽ�������Ͳ�����ԭ��dynamicĬ��ģʽ������Ӱ�죬���Խ���������0
        }  
    }

    private void OnMouseUp()//���̧��ʱ
    {
        if(canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f); //Invoke�����þ��ǽ�Fly�����ӳ�0.1����ִ�У����f��ʾ0.1��float���ͣ�����Ҫд
                                 //�ӳٺ���ִ����������Ϳ�����С���ȡһ����ԭ��dynamicģʽ�µ�һ�����������ȡȫ����
                                 //��Ϊȫ������û�м�����ɾͱ���Ϊ����ѧģʽ��
            right.enabled = false;
            left.enabled = false;//�ڷɳ���ʱ����������������Ҫ���������ǹ��Ų���
            canMove = false;
        }
    }

    private void Fly()
    {
        isFly = true;
        MusicPlay(fly);
        myTrail.TrailStarts();
        sp.enabled = false;//����sp����ʾ�������������ģ�ͣ������ɿ����Ͳ����е���������С����
        Invoke("NextBird", 5f);
    }

    protected virtual void NextBird()
    {
        GameManagers.instance.bIrds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManagers.instance.PreNextBird();
    }

    private void Line()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position); //���Ǹ�right����������������㣬0��1��ʾ���ǵڼ�����
        right.SetPosition(1, transform.position);//�ڶ���������ʾ������λ��
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.ClearTrails();
        
    }

    public void MusicPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public virtual void ShowSkill()
    {
        isFly = false;
    }

    public void Hurt()
    {
        render.sprite = hurt;
    }
}
