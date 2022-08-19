using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("Player")]
	[SerializeField] private PlayerController playerController;
	public Transform PlayerTransfrom { get { return playerController.transform; } }

	[Header("Enemy Spawn")]
	[SerializeField] private EnemyController enemyController;

	//singleton Instance
	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }

	private void Awake()
	{
		if (instance != null)
			Destroy(gameObject);
		else
			instance = this;
	}
}
