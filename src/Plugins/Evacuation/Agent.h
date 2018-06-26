#ifndef __EVACUATION_AGENT_H__
#define __EVACUATION_AGENT_H__

#include "MengeCore\Orca\ORCAAgent.h"
#include <unordered_set>

namespace Evacuation
{
	class Agent : public ORCA::Agent
	{
	public:
		std::unordered_set<std::string> _dead_ends;
	};
}

#endif // !__EVACUATION_AGENT_H__
