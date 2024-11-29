using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    CharacterController con;
    Animation anim;

    float normalSpeed = 3f; //�ʏ펞�̈ړ����x
    float sprintSpeed = 5f; //�_�b�V�����̈ړ����x
    float jump = 8f;        //�W�����v��
    float gravity = 10f;    //�d�͂̑傫��

    Vector3 moveDirection=Vector3.zero;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        con = GetComponent & lt;CharacterController & gt; ();
        anim = GetComponent & lt; Animator & gt; ();

        //�}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ����x���擾
        float speed=Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

        //�J�����̌�������ɂ������ʕ����̃x�N�g��
        Vector3 cameraForward=Vector3.Scale(Camera.main.transform.forward,new Vector3(1,0,1)).normalized;

        //�O�㍶�E�̓��́iWASD�L�[�j����A�ړ��̂��߂̃x�N�g�����v�Z
        //Input.GetAxis("Vertical")�͑O��̓��͒l
        //Input.GetAxis("Horizontal")�͍��E�̓��͒l
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed;

        //isGrounded�͒n�ʂɂ��邩�ǂ����𔻒肵�܂�
        //�n�ʂɂ���Ƃ��̓W�����v���\��
        if (con.isGrounded)
        {
            moveDirection = moveZ + moveX;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jump;
            }
        }
        else
        {
            //�d�͂���������
            moveDirection=moveZ+moveX+new Vector3(0,moveDirection.y,0);
            moveDirection.y-=gravity*Time.deltaTime;
        }

        //�ړ��̃A�j���[�V����
        anim.SetFloat("MoveSpeed",(moveZ+moveX).magnitude);

        //�v���C���[�̌�������͂̌����ɕύX
        

    }
}
