using UnityEngine;
using System.Collections.Generic;

public interface AStarInterface {
	void setCosts(float costs);
	float getCosts();
	void setPathcosts(float costs);
	float getPathcosts();
	void setHeuristiccosts(float costs);
	float getHeuristiccosts();
	void setNeighbour(AStarInterface t);
	List<AStarInterface> getNeighbours();
	void setPrev(AStarInterface prev);
	AStarInterface getPrev();
	void setPosX(int x);
	int getPosX();
	void setPosY(int y);
	int getPosY();
	void setPlaneObject(GameObject o);
	GameObject getPlaneObject();
}
