#include "UnknownPathGoalSelector.h"
#include "MengeCore\Agents\BaseAgent.h"
#include "MengeCore\Math\RandGenerator.h"
#include "Agent.h"
#include "GoalFactory.h"
#include <stdlib.h>
#include <time.h>

namespace Evacuation
{
	Goal * UnknownPathGoalSelector::getGoal(const BaseAgent * agent) const
	{
		Agent * a = (Agent*)agent;

		if (!a->_lastGoal)
		{
			return a->_lastGoal = (EvacuationAABBGoal*)_goalSet->getRandomWeightedGoal();
		}

		srand(time(NULL));

		//TODO: dead ends

		return a->_lastGoal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_lastGoal->_adjacent[rand() % a->_lastGoal->_adjacent.size()]);
	}
}