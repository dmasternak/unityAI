using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : AStarInterface{
	private bool isWalkable = true;
	private float costs     = 1.0f;
	private List<AStarInterface> neighbours = new List<AStarInterface>();

	public float pathcosts = 0.0f;
	public float heuristiccost = 0.0f;

	public int posX = 0;
	public int posY = 0;

	public GameObject plane = null;

	// later for backtracking
	public AStarInterface prev = null;

	public Tile(int posX, int posY) {
		this.posX = posX;
		this.posY = posY;
		this.plane = null;
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

	public void setNeighbour(AStarInterface t) {
		this.neighbours.Add(t);
	}

	public List<AStarInterface> getNeighbours() {
		return this.neighbours;
	}

	public void setPathcosts(float pathcosts) {
		this.pathcosts = pathcosts;
	}
	
	public float getPathcosts() {
		return this.pathcosts;
	}
	
	public void setHeuristiccosts(float heuristiccosts) {
		this.heuristiccost = heuristiccosts;
	}
	
	public float getHeuristiccosts() {
		return this.heuristiccost;
	}

	public void setPrev(AStarInterface prev) {
		this.prev = prev;
	}
	
	public AStarInterface getPrev() {
		return this.prev;
	}
	
	public void setPosX(int x) {
		this.posX = x;
	}
	
	public int getPosX() {
		return this.posX;
	}
	
	public void setPosY(int y) {
		this.posY = y;
	}
	
	public int getPosY() {
		return this.posY;
	}

	public void setPlaneObject(GameObject plane) {
		this.plane = plane;
	}

	public GameObject getPlaneObject() {
		return this.plane;
	}
}
