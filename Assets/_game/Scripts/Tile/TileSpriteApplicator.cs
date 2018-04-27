using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSpriteApplicator : MonoBehaviour {

	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Tile tile;

	void Start() {
		ApplyTileSprite();
		gameObject.AddComponent<PolygonCollider2D>();
	}

	void ApplyTileSprite() {
		spriteRenderer.sprite = tile.sprite;
		spriteRenderer.color = tile.color;
	}
}
