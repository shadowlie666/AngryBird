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
    public LineRenderer right; //一个画线的组件
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
        if (isClick)//如果鼠标一直按下
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//将小鸟位置与鼠标位置同步
            transform.position += new Vector3(0, 0, 10);//2d游戏要消除z轴的同步

            if(Vector3.Distance(transform.position,rightPos.position)>maxDis)//限制小鸟与弹弓之间拉伸的最大距离
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化弹弓与小鸟之间的向量，即获取方向
                pos *= maxDis;//给这个单位向量赋予长度，即限制了这个方向上弹弓与鸟的最大长度
                transform.position = rightPos.position + pos;//限制小鸟在当前方向上只能在这个位置待着，不能在往外走了
            }
            Line();
        }

        //相机跟随
        //其实现方式就是利用线性插值函数让相机的x坐标往小鸟的x坐标那里平滑移动（不能马上移动过去，所以要使用线性插值才能平滑移动）
        //其中的clamp是限制相机跟随的，因为不能小鸟去哪相机都跟着，当小鸟飞的远了相机就不跟了，其意思就是如果x在0-15那么x就是它的值本身，如果不在这个区间那么x不是0就是15
        //time.deltatime是一秒，因为游戏运行的帧数会变化，所以一帧的时间可能是1/30或者1/60之类的，time.deltatime=一帧的秒数 * 一秒的帧数=一秒，这样无论游戏多少帧都一定是一秒
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

    private void Awake()//通过awake方法让这些组件变量实例化，这样他们就表示当前物体的这个组件
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()//鼠标按下时
    {
        if(canMove)
        {
            MusicPlay(select);
            isClick = true;
            rg.isKinematic = true;//将刚体模型切换为动力学模式，这样就不会受原先dynamic默认模式的力的影响，可以将其受力归0
        }  
    }

    private void OnMouseUp()//鼠标抬起时
    {
        if(canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f); //Invoke的作用就是将Fly函数延迟0.1秒再执行，这个f表示0.1是float类型，必须要写
                                 //延迟后再执行这个函数就可以让小鸟获取一部分原先dynamic模式下的一点力，不会获取全部力
                                 //因为全部力还没有计算完成就被换为动力学模式了
            right.enabled = false;
            left.enabled = false;//在飞出的时候禁用绳子组件，不要让绳子在那挂着不动
            canMove = false;
        }
    }

    private void Fly()
    {
        isFly = true;
        MusicPlay(fly);
        myTrail.TrailStarts();
        sp.enabled = false;//禁用sp所表示的组件，即弹力模型，这样松开鼠标就不用有弹簧绳拉着小鸟了
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
        right.SetPosition(0, rightPos.position); //就是给right这个变量设置两个点，0，1表示这是第几个点
        right.SetPosition(1, transform.position);//第二个参数表示这个点的位置
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
