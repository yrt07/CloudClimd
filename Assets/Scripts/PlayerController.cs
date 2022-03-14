using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//LoadSceneを使うために必要！！
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkforce = 30.0f;
    float maxWalkSpeed = 2.0f;
    public int movecount;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        movecount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //ジャンプする
        if (Input.GetKeyDown(KeyCode.Space) &&
                this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            movecount++;
            Debug.Log(movecount);
        }

        //左右移動
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        //プレイヤーの速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //スピード制限
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkforce);
        }

        //動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //プレイヤーの速度に応じてアニメーション速度を変える
        if(this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }
        this.animator.speed = speedx / 2.0f;


        //画面外に出た場合は最初から
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    //ゴールに到達
    private void OnTriggerEnter2D(Collider2D othier)
    {
        Debug.Log("ゴール");
        SceneManager.LoadScene("ClearScene");

        float gametimer = GameObject.Find("GameDirector").GetComponent<GameDirector>().getgametimer();
        Debug.Log(gametimer.ToString("F2") + "秒");
        PlayerPrefs.SetFloat("playtime", gametimer);
        if(PlayerPrefs.HasKey("besttime")==false)//ベストタイムのキー持ってるか聞いてる
        {
            PlayerPrefs.SetFloat("besttime", gametimer);
        }
        else
        {
            float besttime = PlayerPrefs.GetFloat("besttime");
            if(gametimer < besttime)
            {
                PlayerPrefs.SetFloat("besttime", gametimer);

            }
        }
        PlayerPrefs.Save();//入れ物にいれて保存

    }
}
