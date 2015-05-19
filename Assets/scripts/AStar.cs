using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AStar {
	List<AStarInterface> openList;
	List<AStarInterface> closedList;

	public AStar() {
		this.openList = new List<AStarInterface>();
		this.closedList = new List<AStarInterface>();
	}

	public AStarInterface calcAStar(AStarInterface start, AStarInterface end) {
		openList.Add(start);

		while(openList.Count > 0) {
			// here we have to sort the list!!!!!
			AStarInterface currentTile = openList.First(); // get key on position 0

			openList.RemoveAt(0);

			if(currentTile == end) 
				return currentTile;

			closedList.Add(currentTile);

			expandNodes(currentTile, end);
		}

		return null;
	}

	public void expandNodes(AStarInterface t, AStarInterface end) {
		foreach(AStarInterface n in t.getNeighbours()) {
			if(this.closedList.Contains(n)) 
				continue;

			float newCosts = n.getCosts() + t.getPathcosts();

			/*foreach(Tile tmp in openList) {
				Debug.Log(tmp.getCosts());
			}*/

			if(this.openList.Contains(n) && newCosts >= n.getPathcosts())
				continue;

			n.setPrev(t);
			n.setPathcosts(newCosts);

			float f = newCosts + calcHeuristic(n, end);

			if(openList.Contains(n)) {
				n.setHeuristiccosts(f);
				openList.Sort((x,y) => x.getHeuristiccosts().CompareTo(y.getHeuristiccosts()));
			}
			else {
				n.setHeuristiccosts(f);
				openList.Add(n);
				openList.Sort((x,y) => x.getHeuristiccosts().CompareTo(y.getHeuristiccosts()));
			}

			/*foreach(Tile tmp in openList) {
				Debug.Log(tmp.pathcosts);
			}*/
		}
	}

	public float calcHeuristic(AStarInterface from, AStarInterface to) {
		float res = Mathf.Sqrt((to.getPosX() - from.getPosX())*(to.getPosX() - from.getPosX()) + 
		                       (to.getPosY() - from.getPosY())*(to.getPosY() - from.getPosY()));
		/*Debug.Log("from[x,y]: " + from.tileX + ", " + from.tileZ 
		          + " to[x,y]: " + to.tileX + ", " + to.tileZ
		          + " res: " + res
		          );*/
		return res;
	}
}
