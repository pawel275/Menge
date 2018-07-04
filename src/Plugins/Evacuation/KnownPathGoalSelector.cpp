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
			const size_t GOAL_COUNT = _goalSet->size();
			const Vector2 p = agent->_pos;

			Goal * bestGoal;

			bestGoal = _goalSet->getIthGoal(0);
			Vector2 disp = bestGoal->getCentroid() - p;
			float bestDist = absSq(disp);
			for (size_t i = 1; i < GOAL_COUNT; ++i) {
				Goal * testGoal = _goalSet->getIthGoal(i);
				disp = testGoal->getCentroid() - p;
				float testDist = absSq(disp);
				if (testDist < bestDist) {
					bestDist = testDist;
					bestGoal = testGoal;
				}
			}

			return a->_lastGoal = (EvacuationAABBGoal*)bestGoal;
		}

		return a->_lastGoal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_lastGoal->_next);
	}
}
