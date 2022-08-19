using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("KeyBinds")]
	[SerializeField] private KeyCode m_ForwardKey;
	[SerializeField] private KeyCode m_LeftKey;
	[SerializeField] private KeyCode m_RightKey;

	[Header("Movement Properties")]
	[SerializeField] private float m_MoveSpeed;
	[SerializeField] private float m_MaxMoveSpeed;
	[SerializeField] private float m_RotationSpeed;

	[Header("Componenet References")]
	[SerializeField] private Rigidbody m_Rigidbody;
	[SerializeField] private Animator m_Animator;

	private bool b_MoveForward;
	private bool b_MoveLeft;
	private bool b_MoveRight;

	private int anim_VelocityZ;

	private void Start()
	{
		anim_VelocityZ = Animator.StringToHash("VelocityZ");
	}

	private void Update()
	{
		GetInput();
		m_Animator.SetFloat(anim_VelocityZ, m_Rigidbody.velocity.magnitude);
	}

	private void GetInput()
	{
		b_MoveForward = Input.GetKey(m_ForwardKey);
		b_MoveLeft = Input.GetKey(m_LeftKey);
		b_MoveRight = Input.GetKey(m_RightKey);
	}

	private void FixedUpdate()
	{
		ForwardMovement();

		Rotation();
	}

	private void Rotation()
	{
		if (b_MoveLeft || b_MoveRight)
		{
			transform.localEulerAngles += (b_MoveLeft ? -1 : 1) * m_RotationSpeed * Time.fixedDeltaTime * transform.up;
		}
	}

	private void ForwardMovement()
	{
		if (b_MoveForward && (Mathf.Abs(m_Rigidbody.velocity.z) < m_MaxMoveSpeed))
		{
			m_Rigidbody.velocity += m_MoveSpeed * Time.fixedDeltaTime * transform.forward;
		}		
	}
}
