using JKFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerCombat : Combat, ICombat
    {
        //Components
        Rigidbody2D m_Rigid2D;
        AnimatorController m_AnimatorController;
        PlayerController m_PlayerController;
        bool m_CanCombo;

        public void OnEnable()
        {
            m_PlayerController = GetComponentInParent<PlayerController>();
            m_Rigid2D = GetComponentInParent<Rigidbody2D>();
            m_AnimatorController = GetComponent<AnimatorController>();

            //Add hit event
            hitInfo += HitDetected;
            JKInputSystem.AddListenerActionX(AttackCombo);
        }

        public void OnDisable()
        {
            JKInputSystem.RemoveListenerActionX(AttackCombo);
        }

        //Hit method
        public override void HitDetected()
        {
            if (ColliderDetected.gameObject.tag == "Enemy")//if is enemy
            {
                Stats enemyStats = ColliderDetected.GetComponent<Stats>(); //Get data component from object
                                                                          
                //Make visual hit effect
                HitEffect hitEffect = ColliderDetected.GetComponentInChildren<HitEffect>();
                hitEffect.PlayEffect();

                float damage = damageRange.Random(); //get 1 random value damage of 2 (min,max)

                MeleeAttack(enemyStats, damage);

                CameraManager.Instance.cameraShake.Shake(); //Damage
            }
        }
        //Method for animator, when player melee attack begin
        public void OnMeleeAttackBegin(float timeToCombo)
        {
            StartCoroutine(ICombo(timeToCombo)); //Start combo system
        }

        public void AttackForce(float forcePower)
        {
            m_Rigid2D.AddForce(Vector2.right * transform.localScale.x * forcePower); //Makes jerk when attacking
        }

        //Method for animator, when player melee attack end
        public void OnMeleeAttackEnd()
        {
            StopCoroutine(ICombo(0)); //Stop combo 
            //Animator update
            m_Rigid2D.velocity = Vector2.zero;
            m_AnimatorController.ResetTrigger("AttackCombo");
            m_AnimatorController.SetBool("MeleeAttack", false);

            m_CanCombo = false; //block combo
            m_PlayerController.isAttacking = false;
        }

        public void OnRangeAttackEnd()
        {
            m_Rigid2D.velocity = Vector2.zero;
            m_AnimatorController.SetBool("RangeAttack", false);
            m_PlayerController.isAttacking = false;
        }

        //Combo system
        IEnumerator ICombo(float comboTimer)
        {
            m_CanCombo = false;
            yield return new WaitForSeconds(comboTimer);
            m_CanCombo = true;
        }

        void AttackCombo()
        {
            if (m_CanCombo)
            {
                m_CanCombo = false;
                m_AnimatorController.SetTrigger("AttackCombo");
                StopCoroutine(ICombo(0));
            }
        }
    }
}
