#ifndef __EVACUATION_DB_ENTRY_H__
#define	__EVACUATION_DB_ENTRY_H__

#include "Simulator.h"
#include "MengeCore\Orca\ORCADBEntry.h"
#include "MengeCore\Agents\SimulatorInterface.h"
#include "MengeCore\Orca\ORCA.h"

namespace Evacuation
{
	class DBEntry : public ORCA::DBEntry
	{
	public:

		virtual ::std::string commandLineName() const override
		{
			return "evacuation";
		}

		virtual Menge::Agents::SimulatorInterface * getNewSimulator() override
		{
			return new Simulator();
		}

	};
}
#endif
