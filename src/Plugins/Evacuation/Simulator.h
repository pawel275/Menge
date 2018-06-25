#ifndef __EVACUATION_SIMULATOR_H__
#define __EVACUATION_SIMULATOR_H__

#include "Agent.h"
#include "MengeCore\Agents\SimulatorBase.h"

namespace Evacuation 
{
	class Simulator : public Menge::Agents::SimulatorBase<Agent>
	{
	};
}

#endif // !__EVACUATION_SIMULATOR_H__
