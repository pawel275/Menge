#ifndef __EVACUATION_AGENT_H__
#define __EVACUATION_AGENT_H__

#include "MengeCore\Orca\ORCAAgent.h"
#include "GoalFactory.h"
#include <unordered_set>

namespace Evacuation
{
	class Agent : public ORCA::Agent
	{
	public:
		size_t _start_goal_id;
		EvacuationAABBGoal * _lastGoal = nullptr;
		EvacuationAABBGoal * _secondLastGoal = nullptr;
		std::unordered_set<size_t> _dead_ends;
	};
}

#endif // !__EVACUATION_AGENT_H__
