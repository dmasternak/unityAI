using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AStar {
	List<Tile> openList;
	List<Tile> closedList;

	public AStar() {
		this.openList = new List<Tile>();
		this.closedList = new List<Tile>();
	}

	public Tile calcAStar(Tile start, Tile end) {
		openList.Add(start);

		while(openList.Count > 0) {
			// here we have to sort the list!!!!!
			Tile currentTile = openList.First(); // get key on position 0

			openList.RemoveAt(0);

			if(currentTile == end) 
				return currentTile;

			closedList.Add(currentTile);

			expandNodes(currentTile, end);
		}

		return null;
	}

	public void expandNodes(Tile t, Tile end) {
		foreach(Tile n in t.getNeighbours()) {
			if(this.closedList.Contains(n)) 
				continue;

			float newCosts = n.getCosts() + t.pathcosts;

			/*foreach(Tile tmp in openList) {
				Debug.Log(tmp.getCosts());
			}*/

			if(this.openList.Contains(n) && newCosts >= n.pathcosts)
				continue;

			n.prev = t;
			n.pathcosts = newCosts;

			float f = newCosts + calcHeuristic(n, end);

			if(openList.Contains(n)) {
				n.heuristiccost = f;
				openList.Sort((x,y) => x.heuristiccost.CompareTo(y.heuristiccost));
			}
			else {
				n.heuristiccost = f;
				openList.Add(n);
				openList.Sort((x,y) => x.heuristiccost.CompareTo(y.heuristiccost));
			}

			/*foreach(Tile tmp in openList) {
				Debug.Log(tmp.pathcosts);
			}*/
		}
	}

	public float calcHeuristic(Tile from, Tile to) {
		float res = Mathf.Sqrt((to.tileX - from.tileX)*(to.tileX - from.tileX) + 
		                       (to.tileZ - from.tileZ)*(to.tileZ - from.tileZ));
		/*Debug.Log("from[x,y]: " + from.tileX + ", " + from.tileZ 
		          + " to[x,y]: " + to.tileX + ", " + to.tileZ
		          + " res: " + res
		          );*/
		return res;
	}
}
