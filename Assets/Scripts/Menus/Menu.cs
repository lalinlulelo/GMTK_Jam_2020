﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	[SerializeField] bool quitOnEscape = false;
	[SerializeField] private Texture2D cursor = null;
	[SerializeField] private Vector2 cursorHotspot = Vector2.zero;

	protected virtual void Awake()
	{
		Application.targetFrameRate = 60;
		Cursor.SetCursor(cursor, cursorHotspot, CursorMode.ForceSoftware);
	}

	private void Update()
	{
		if (quitOnEscape && Input.GetKeyDown(KeyCode.Escape))
		{
			Event_Quit();
		}
	}

	public void Event_GotoScene(string scene)
	{
		SceneCurtain.ChangeScene(scene);
	}

	public virtual void Event_Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
