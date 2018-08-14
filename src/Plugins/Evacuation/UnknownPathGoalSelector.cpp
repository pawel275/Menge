#include "UnknownPathGoalSelector.h"
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
	Goal * UnknownPathGoalSelector::getGoal(const BaseAgent * agent) const
	{
		srand(time(NULL));
		
		Agent * a = (Agent*)agent;

		if (!a->_last_goal)
		{
			a->_last_goal = (EvacuationAABBGoal*)_goalSet->getGoalByID(a->_start_goal_id);
		}
		else
		{
			if (a->_last_goal->_adjacent.size() == 1)
			{
				a->_dead_ends.insert(a->_last_goal->getID());
			}

			std::vector<size_t> choices;

			for (std::vector<size_t>::iterator it = a->_last_goal->_adjacent.begin(); it != a->_last_goal->_adjacent.end(); ++it)
			{
				if (a->_dead_ends.find(*it) == a->_dead_ends.end() && (a->_second_last_goal ? *it != a->_second_last_goal->getID() : true)) 
				{
					choices.push_back(*it);
				}
			}

			if (choices.empty()) 
			{
				choices.push_back(a->_second_last_goal->getID());
				a->_dead_ends.insert(a->_last_goal->getID());
			}

			a->_second_last_goal = a->_last_goal;
 			a->_last_goal = (EvacuationAABBGoal*)_goalSet->getGoalByID(choices[rand() % choices.size()]);
		}

		return a->_last_goal;
	}
}