using UnityEngine;
using Invector.vCharacterController;    // 引用 套件

public class Player : MonoBehaviour
{
    private float hp = 100;
    private Animator ani;
    /// <summary>
    /// 連擊次數
    /// </summary>
    private int atkCount;

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    //增加變數並界定此變數之範圍。這個變數可以在Inspector調整。
    [Header("連擊間隔時間"), Range(0, 3)]
    public float interval = 1;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 30;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    /// <summary>
    /// 繪製圖示事件：僅在 Unity 內顯示
    /// </summary>
    private void OnDrawGizmos()
    {
        // 圖示.顏色 = 青色
        Gizmos.color = Color.cyan;
        // 圖示.繪製射線(中心點，方向)
        // (攻擊中心點的座標，攻擊中心點的前方 * 攻擊長度)
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);
    }

    //將"被設限擊中的物件"命名為hit
    /// <summary>
    /// 射線擊中的物件
    /// </summary>
    private RaycastHit hit;

    //創造一個名為Atack的事件
    private void Attack()
    {
        if (atkCount < 3)//如果 連擊次數小於3 則執行以下程式
        {
            if (timer < interval)//如果計時器小於連擊間隔時間
            {
                timer += Time.deltaTime;//則計時器增加一幀

                if (Input.GetKeyDown(KeyCode.Mouse0)) //如果按下(Mouse0)按鍵 註 M0左鍵 M1右鍵 M2中鍵
                {
                    atkCount++;//則連擊次數遞增
                    timer = 0;//並且計時器歸零

                    // 物理.射線碰撞(攻擊中心點的座標，攻擊中心點的前方，射線擊中的物件，攻擊長度，圖層)
                    // 圖層：1 << 圖層編號 (只有此圖層內的物件會被這個事件影響)
                    // 如果 (射線 打到物件()) 就 執行 {  }
                    if (Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atkLength, 1 << 9))
                    {
                        // 碰撞物件.取得元件<敵人>().受傷事件()
                        hit.collider.GetComponent<Enemy>().Damage(atk);
                    }

                }
            }
            
            else//否則(意指計時器大於等於連擊間隔時間)
            {
                atkCount = 0;//連擊次數歸零
                timer = 0;//計時器歸零
            }

        }

        if (atkCount == 3)//如果 連擊次屬等於3
        {
            atkCount = 0;//則 連擊次數歸零
        }

        ani.SetInteger("攻擊", atkCount);//播放名為"攻擊"的動畫

    }
    /// <summary>
    /// 角色.受傷事件
    /// </summary>
    public void Damage(float Damage)
    {
        hp -= Damage;
        ani.SetTrigger("受傷觸發");

        if (hp <= 0) Dead();
    }

    /// <summary>
    /// 角色.死亡事件
    /// </summary>
    private void Dead()
    {

        ani.SetBool("死亡開關", true);

        // 鎖定移動與旋轉
        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }
}
