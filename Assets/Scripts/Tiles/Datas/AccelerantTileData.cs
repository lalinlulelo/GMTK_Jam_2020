﻿using System.Runtime.InteropServices;
using Grid;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "AccelerantTileData", menuName = "ScriptableObjects/TileDatas/AccelerantTileData")]
public class AccelerantTileData : TileData
{
	[SerializeField] private float detonateAfter = 1;
	public float DetonateAfter => detonateAfter;

	[SerializeField] private bool rematchOnFall = false;

	public override bool IsMatch(TileData other, Tile targetTile)
	{
		if (other is AccelerantTileData)
		{
			return true;
		}

		if (other != null && targetTile != null)
		{
			var pin = targetTile.GetComponent<AccelerantPin>();
			if (pin == null)
			{
				return false;
			}
			else if(other is ShapeTileData)
			{
				return pin.Ticking;
			}
		}

		return false;
	}

	protected override void burn(Tile target)
	{
		if (target != null)
		{
			var accelerantPin = target.GetComponent<AccelerantPin>();
			if (accelerantPin == null)
			{
				accelerantPin = target.gameObject.AddComponent<AccelerantPin>();
			}
			accelerantPin.SetData(this);
			accelerantPin.ResetTime();
		}
	}

	public override void OnSpawn(Tile target, TileGrid grid)
	{
		base.OnSpawn(target, grid);
	}

	public override void FallIntoPlace(Tile target)
	{
		if (rematchOnFall)
		{
			// This is probably a terrible idea, but it might cause more chain reactions.
			burn(target);
		}
	}
}
