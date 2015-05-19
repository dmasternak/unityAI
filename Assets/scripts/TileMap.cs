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
				map[x, y].createPlaneTile(x, 0, y, Color.green);
			}
		}

		Destroy(map[4, 4].plane);
		map[4, 4].createPlaneTile(4, 0, 4, Color.blue);

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
		Tile res = astar.calcAStar(map[0, 0], map[9, 9]);

		if(res != null) {
			Debug.Log("Found path");

			Tile tmp = res;

			while(tmp != null) {
				Debug.Log("tile[x,y]: " + tmp.tileX + ", " + tmp.tileZ);

				if(tmp.prev != null)
					Debug.DrawLine(new Vector3(tmp.tileX, 1, tmp.tileZ),
				    	           new Vector3(tmp.prev.tileX, 1, tmp.prev.tileZ),
				        	       Color.red, 20000.0f, false);

				tmp = tmp.prev;
			}
		}
	}

	void Update() {
		//Debug.DrawLine (new Vector3(1 , 1, 1), new Vector3 (2, 2, 2), Color.red);
	}

	public Tile[,] getMap() {
		return this.map;
	}
}
