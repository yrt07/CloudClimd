using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//LoadScene���g�����߂ɕK�v�I�I
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
        //�W�����v����
        if (Input.GetKeyDown(KeyCode.Space) &&
                this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            movecount++;
            Debug.Log(movecount);
        }

        //���E�ړ�
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        //�v���C���[�̑��x
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //�X�s�[�h����
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkforce);
        }

        //���������ɉ����Ĕ��]
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //�v���C���[�̑��x�ɉ����ăA�j���[�V�������x��ς���
        if(this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }
        this.animator.speed = speedx / 2.0f;


        //��ʊO�ɏo���ꍇ�͍ŏ�����
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    //�S�[���ɓ��B
    private void OnTriggerEnter2D(Collider2D othier)
    {
        Debug.Log("�S�[��");
        SceneManager.LoadScene("ClearScene");

        float gametimer = GameObject.Find("GameDirector").GetComponent<GameDirector>().getgametimer();
        Debug.Log(gametimer.ToString("F2") + "�b");
        PlayerPrefs.SetFloat("playtime", gametimer);
        if(PlayerPrefs.HasKey("besttime")==false)//�x�X�g�^�C���̃L�[�����Ă邩�����Ă�
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
        PlayerPrefs.Save();//���ꕨ�ɂ���ĕۑ�

    }
}
