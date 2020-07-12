﻿using System.Collections;
using System.Reflection;
using Levels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
	public LevelManager levelManager;
	[SerializeField] private LevelSelectItem listItem = null;
	[SerializeField] private Transform container = null;
	public string gameScene = "Game";

	private LevelSelectItem defaultItem = null;

	void Awake()
	{
		for (int i = 0; i < levelManager.Levels.Length; i++)
		{
			var level = levelManager.Levels[i];
			var item = Instantiate(listItem, container).GetComponent<LevelSelectItem>();
			item.levelSelect = this;
			item.AttachLevel(level, i);

			if (i == levelManager.CurrentLevel)
			{
				defaultItem = listItem;
			}
		}

		StartCoroutine(pickDefault());
	}

	IEnumerator pickDefault()
	{
		yield return null;
		EventSystem.current.SetSelectedGameObject(null);
		yield return null;
		EventSystem.current.SetSelectedGameObject(defaultItem.gameObject);
		yield return null;
		defaultItem.GetComponent<Button>().Select();
	}
}
