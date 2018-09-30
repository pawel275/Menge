#include "UnknownPathGoalSelector.h"
#include "MengeCore\Agents\BaseAgent.h"
#include "MengeCore\Math\RandGenerator.h"
#include "Agent.h"
#include "GoalFactory.h"
#include <stdlib.h>
#include <time.h>
#include <unordered_set>
#include "Menge/MengeCore/Math/Vector2.h"

using Menge::Math::Vector2;

namespace Evacuation
{
	Goal * UnknownPathGoalSelector::getGoal(const BaseAgent * agent) const
	{
		srand(time(NULL));
		
		Agent * a = (Agent*)agent;

		if (!a->_current_goal)
		{
			a->_current_goal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_start_goal_id);
		}
		else
		{
			std::vector<size_t> choices;

			for each (size_t goal in a->_current_goal->_adjacent)
			{
				// goal is not a dead end && goal != last goal
				if (a->_dead_ends.find(goal) == a->_dead_ends.end() && (a->_last_goal ? goal != a->_last_goal->getID() : true)) 
				{
					choices.push_back(goal);
				}
			}

			if (choices.empty()) 
			{
				choices.push_back(a->_last_goal->getID());
				a->_dead_ends.insert(a->_current_goal->getID());
			}
			// only 1 choice && last goal is a dead end
			else if (a->_last_goal && choices.size() == 1 && a->_dead_ends.count(a->_last_goal->getID()))
			{
				a->_dead_ends.insert(a->_current_goal->getID());
			}

			a->_last_goal = a->_current_goal;
 			a->_current_goal = (EvacuationAABBGoal*)_goalSet->getGoalByID(choices[rand() % choices.size()]);
		}

		return a->_current_goal;
	}
}