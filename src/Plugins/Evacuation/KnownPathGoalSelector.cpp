#include "KnownPathGoalSelector.h"
#include "MengeCore\Agents\BaseAgent.h"
#include "MengeCore\Math\RandGenerator.h"
#include "Agent.h"
#include "GoalFactory.h"
#include <stdlib.h>
#include <time.h>
#include "Menge/MengeCore/Math/Vector2.h"

using Menge::Math::Vector2;

namespace Evacuation
{
	Goal * KnownPathGoalSelector::getGoal(const BaseAgent * agent) const
	{
		Agent * a = (Agent*)agent;

		if (!a->_last_goal)
		{
			return a->_last_goal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_start_goal_id);
		}

		return a->_last_goal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_last_goal->_next);
	}
}
