using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float m_MoveSpeed;

	[Header("Range")]
	[SerializeField] private float m_DetectionRange;
	[SerializeField] private float m_AttackRange;

	[Header("Component Reference")]
	[SerializeField] private Rigidbody m_Rigidbody;
	[SerializeField] private Animator m_Animator;

	private Transform target;
	private float m_DistanceToPlayer;

	private int anim_VelocityZ;
	private int anim_Attack;

	private bool b_IsAttacking;

	private void Start()
	{
		target = GameManager.Instance.PlayerTransfrom;
		anim_VelocityZ = Animator.StringToHash("VelocityZ");
		anim_Attack = Animator.StringToHash("Attack");
	}

	private void Update()
	{
		m_DistanceToPlayer = Vector3.Distance(transform.position, target.position);
		StateHandler();
	}

	private void StateHandler()
	{
		if (m_DistanceToPlayer < m_DetectionRange)
		{
			transform.LookAt(target.position);
			if (m_DistanceToPlayer < m_AttackRange)
			{
				SetRunAnim(0);
				Attack();
				return;
			}
			Chase();
		}
		else
		{
			SetRunAnim(0);
		}
	}

	private void Chase()
	{
		if (b_IsAttacking)
			return;
		m_Rigidbody.MovePosition(Vector3.MoveTowards(transform.position, target.position, m_MoveSpeed * Time.deltaTime));
		SetRunAnim(1);
	}

	private void SetRunAnim(float value)
	{
		m_Animator.SetFloat(anim_VelocityZ, value);
	}

	private void Attack()
	{
		if (!b_IsAttacking)
		{
			m_Animator.SetTrigger(anim_Attack);
			StartCoroutine(SetAttacking());
		}
	}

	private IEnumerator SetAttacking()
	{
		b_IsAttacking = true;
		yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorClipInfo(0).Length * 2);
		b_IsAttacking = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.TryGetComponent(out PlayerController controller))
		{
			Debug.Log("Player Hit");
		}
	}
}
