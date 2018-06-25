#include "UnknownPathGoalSelector.h"
#include "MengeCore\Agents\BaseAgent.h"

namespace Evacuation
{
	Goal * UnknownPathGoalSelector::getGoal(const BaseAgent * agent) const
	{
		std::cout << agent->getStringId();
		return _goalSet->getRandomWeightedGoal();
	}
}