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

		if (!a->_lastGoal)
		{
			return a->_lastGoal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_start_goal_id);
		}

		return a->_lastGoal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_lastGoal->_next);
	}
}
