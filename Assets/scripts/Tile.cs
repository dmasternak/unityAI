using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : IComparer<Tile> {
	private bool isWalkable = true;
	private float costs     = 1.0f;
	private List<Tile> neighbours = new List<Tile>();

	public float pathcosts = 0.0f;
	public float heuristiccost = 0.0f;

	public int tileX = 0;
	public int tileZ = 0;

	public GameObject plane = null;

	// later for backtracking
	public Tile prev = null;

	public Tile(int tileX, int tileZ) {
		this.tileX = tileX;
		this.tileZ = tileZ;
	}

	public void createPlaneTile(float posX, float posY, float posZ, Color c){
		plane = new GameObject("tile");
		Vector3 pos = new Vector3(posX, posY, posZ);
		plane.transform.position = pos;

		Quaternion rot = Quaternion.Euler(new Vector3(90, 0, 0));
		plane.transform.rotation = rot;

		MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = createMeshPlane(0.5f, 0.5f);
		MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material.shader = Shader.Find ("Particles/Additive");
		Texture2D tex = new Texture2D(1, 1);
		tex.SetPixel(0, 0, c);
		tex.Apply();
		renderer.material.mainTexture = tex;
	}
	
	public Mesh createMeshPlane(float width, float height) {
		Mesh m = new Mesh();
		m.name = "Tile";
		m.vertices = new Vector3[] {
			new Vector3(-width, -height, 0.01f),
			new Vector3(width, -height, 0.01f),
			new Vector3(width, height, 0.01f),
			new Vector3(-width, height, 0.01f)
		};
		m.uv = new Vector2[] {
			new Vector2 (0, 0),
			new Vector2 (0, 1),
			new Vector2(1, 1),
			new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();
		
		return m;
	}

	public void setIsWalkable(bool isWalkable) {
		this.isWalkable = isWalkable;
	}

	public bool getIsWalkable() {
		return this.isWalkable;
	}

	public void setCosts(float costs) {
		this.costs = costs;
	}

	public float getCosts() {
		return this.costs;
	}

	public void setNeighbour(Tile t) {
		this.neighbours.Add(t);
	}

	public List<Tile> getNeighbours() {
		return this.neighbours;
	}

	public int Compare(Tile x, Tile y) {
		if(x.pathcosts == y.pathcosts) {
			return 1;
		}
		else if(x.pathcosts >= y.pathcosts) {
			return 1;
		}
		else if(x.pathcosts < y.pathcosts) {
			return -1;
		}
		else {
			return 0;
		}
	}
}
