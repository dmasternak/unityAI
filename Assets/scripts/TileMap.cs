using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {
	public int mapSizeX = 10;
	public int mapSizeZ = 10;
	private Tile[,] map;

	// Use this for initialization
	void Start () {
		map = new Tile[mapSizeX, mapSizeZ];
		//createPlaneTile(1, 0, 1, Color.green);
		//createPlaneTile(2, 0, 1, Color.gray);
		for(int x = 0; x < this.mapSizeX; x++) {
			for(int y = 0; y < this.mapSizeZ; y++) {
				map[x, y] = new Tile(x, y);
				createPlaneTile(x, 0, y, Color.green, map[x, y].getPlaneObject());
			}
		}

		Destroy(map[4, 4].getPlaneObject());
		createPlaneTile(4, 0, 4, Color.blue, map[4, 4].getPlaneObject());

		for(int x = 0; x < mapSizeX; x++) {
			for(int y = 0; y < mapSizeZ; y++) {
				if(x > 0)
					map[x, y].setNeighbour(map[x-1, y]);
				if(x < (mapSizeX - 1))
					map[x, y].setNeighbour(map[x+1, y]);
				if(y > 0)
					map[x, y].setNeighbour(map[x, y-1]);
				if(y < (mapSizeZ - 1))
					map[x, y].setNeighbour(map[x, y+1]);
			}
		}

		map[4, 4].setCosts(5);

		AStar astar = new AStar();
		AStarInterface res = astar.calcAStar(map[0, 0], map[9, 9]);

		if(res != null) {
			Debug.Log("Found path");

			AStarInterface tmp = res;

			while(tmp != null) {
				Debug.Log("tile[x,y]: " + tmp.getPosX() + ", " + tmp.getPosY());

				if(tmp.getPrev() != null)
					Debug.DrawLine(new Vector3(tmp.getPosX(), 1, tmp.getPosY()),
				    	           new Vector3(tmp.getPrev().getPosX(), 1, tmp.getPrev().getPosY()),
				        	       Color.red, 20000.0f, false);

				tmp = tmp.getPrev();
			}
		}
	}

	void Update() {
		//Debug.DrawLine (new Vector3(1 , 1, 1), new Vector3 (2, 2, 2), Color.red);
	}

	public Tile[,] getMap() {
		return this.map;
	}
	
	public void createPlaneTile(float posX, float posY, float posZ, Color c, GameObject plane){
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
}
