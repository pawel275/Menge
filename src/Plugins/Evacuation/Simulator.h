#ifndef __EVACUATION_SIMULATOR_H__
#define __EVACUATION_SIMULATOR_H__

#include "Agent.h"
#include "MengeCore\Agents\SimulatorBase.h"

namespace Evacuation 
{
	Menge::Logger simLogger;

	class Simulator : public Menge::Agents::SimulatorBase<Agent>
	{
	public:
		Simulator()
		{
			simLogger.setFile("simLog");
		}
	};
}

#endif // !__EVACUATION_SIMULATOR_H__
