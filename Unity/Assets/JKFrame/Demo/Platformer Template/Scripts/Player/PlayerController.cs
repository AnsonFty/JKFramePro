using UnityEngine;
using JKFrame;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        //Components
        Stats stats;
        AnimatorController animatorController;

        [Header("Variables")]
        public float moveSpeed;
        public float jumpForce;
        public float rollForce;

        Transform playerSprite;
        [HideInInspector] public Rigidbody2D rigid2D { get; set; }

        [SerializeField] bool isGrounded;
        [HideInInspector] public bool isAttacking { get; set; }

        [HideInInspector] public bool onLadder;
        bool isClimping;

        [HideInInspector] public bool CanMove;
        int m_Horizontal;

        private void Start()
        {
            JKInputSystem.StartRole();
            stats = GetComponent<Stats>();
            rigid2D = GetComponent<Rigidbody2D>();
            playerSprite = transform.GetChild(0);
            CanMove = true;
            animatorController = GetComponentInChildren<AnimatorController>();
            //inputManager.inputConfig.UpdateDictionary(); //

            //add Death event
            stats.OnDeath += Death;

            JKInputSystem.AddListenerMove(Movement);
            JKInputSystem.AddListenerStopMove(StopMove);
            JKInputSystem.AddListenerRoleSubmit(Jump);
            JKInputSystem.AddListenerRoleActionX(MeleeAttack);
            JKInputSystem.AddListenerRoleActionY(RangeAttack);

            ConfigManager.Instance.Init();//临时
        }

        public void FixedUpdate()
        {
            Movement();
            //if (GameManager.Instance.isGame && !GameManager.Instance.isPause) //check Game status
            //{
            //Move();
            //LadderClimb();
            GroundCheck();
            //}
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                UISystem.Show<UI_Dialog>().Show("Foreword_1", null);
            }
            //if (GameManager.Instance.isGame && !GameManager.Instance.isPause)
            //{
            //Rotation();
            //Jump();
            //Roll();
            //Attack();
            //Animation();
            //}
        }

        Vector3 RaycaseUp = new Vector2(0, 0.5f);
        Vector2 RaycaseMoveRight = new Vector2(0.01f, 0);

        void Movement()
        {
            if (CanMove && !isAttacking && m_Horizontal != 0)
            {
                RaycastHit2D raycastHit2DUp = Physics2D.Raycast(transform.position + RaycaseUp, m_Horizontal > 0 ? Vector2.right : Vector2.left, 0.4f);
                RaycastHit2D raycastHit2DDown = Physics2D.Raycast(transform.position - RaycaseUp, m_Horizontal > 0 ? Vector2.right : Vector2.left, 0.4f);
                if (raycastHit2DUp.collider == null && raycastHit2DDown.collider == null)
                    transform.Translate(new Vector2(m_Horizontal * moveSpeed * Time.fixedDeltaTime, 0));
            }

        }

        //Move method
        void Movement(Vector2 moveInput)
        {
            MoveDrag(moveInput, 90);
        }

        public void MoveDrag(Vector2 moveInput, float distance = 90)
        {
            if (distance < 90)
            {
                StopMove();
                return;
            }
            if (moveInput.x != 0)
            {
                m_Horizontal = moveInput.x < 0 ? -1 : 1;
                animatorController.SetBool("Move", true);
                if (moveInput.x > 0)
                {
                    playerSprite.localScale = new Vector2(1, 1);
                    return;
                }
                playerSprite.localScale = new Vector2(-1, 1);
            }
        }

        protected void StopMove()
        {
            if (!isClimping)
            {
                m_Horizontal = 0;
                animatorController.SetBool("Move", false);
                rigid2D.velocity = Vector2.zero;
            }
        }

        //Rotation method
        public void Rotation()
        {
            //if (inputManager.Horizontal != 0) //if player any horizontal side move
            //{
            //    if (inputManager.Horizontal < 0)
            //        playerSprite.localScale = new Vector3(-1, 1, 1);
            //    else
            //        playerSprite.localScale = new Vector3(1, 1, 1);
            //}

        }

        //Roll method
        void Roll()
        {
            //if (isGrounded && inputManager.Roll && !isAttacking && !isClimping) //Check for availability rollback
            //{
            //    if (Mathf.Round(inputManager.Horizontal) != 0)
            //    {
            //        animatorController.SetTrigger("Roll");
            //        rigid2D.velocity = Vector2.right * inputManager.Horizontal * rollForce;
            //    }
            //}
        }

        //Jump method
        void Jump()
        {
            if (isGrounded && !isAttacking && !isClimping)
            {
                animatorController.SetTrigger("Jump");
                rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
            }

        }

        public void MeleeAttack()
        {
            if (isGrounded && !isAttacking && !isClimping)
            {
                isAttacking = true; //attack status
                animatorController.SetBool("MeleeAttack", isAttacking); //Set animator bool
            }
        }

        public void RangeAttack()
        {
            if (isGrounded && !isAttacking && !isClimping)
            {
                isAttacking = true; //attack status
                animatorController.SetBool("RangeAttack", isAttacking);
            }
        }

        //Animator method
        public void Animation()
        {
            //if (!isClimping) //Lader check
            //    if (inputManager.Horizontal != 0)
            //        animatorController.SetBool("Move", true);
            //    else
            //        animatorController.SetBool("Move", false);
            //else
            //{
            //    animatorController.SetBool("Move", false);
            //}

            //animatorController.SetBool("isGrounded", isGrounded);
        }

        void LadderClimb()
        {
            if (onLadder) //check lader status
            {
                //if (inputManager.Vertical != 0)
                //{
                //    isClimping = true;
                //    rigid2D.velocity = Vector2.up * inputManager.Vertical * moveSpeed; //move up or down
                //}
                //else
                //{
                //    rigid2D.velocity = Vector2.zero;
                //}

            }
            else //if leave ladder
            {
                isClimping = false;
            }

            if (onLadder)
                rigid2D.gravityScale = 0;
            else
                rigid2D.gravityScale = 1;
        }

        //Death event
        public void Death()
        {
            animatorController.SetTrigger("Death");
            //GameManager.Instance.GameOver(); //set game state to game over
        }

        //Check ground 
        void GroundCheck()
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, 1f);
            if (raycastHit2D.collider != null)
            {
                Debug.DrawLine(transform.position, raycastHit2D.point, Color.red); //draw line only in editor
                if (!isGrounded)
                    rigid2D.velocity = Vector2.zero;
                if (Vector2.Distance(transform.position, raycastHit2D.point) <= raycastHit2D.distance) //if raycast 
                {
                    isGrounded = true; //is ground
                    animatorController.SetBool("isGrounded", isGrounded);
                }
                return;
            }

            isGrounded = false;
            animatorController.SetBool("isGrounded", isGrounded);
        }
    }
}
