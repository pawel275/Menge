#ifndef __EVACUATION_GOAL_FACTORY_H__
#define __EVACUATION_GOAL_FACTORY_H__

#include <unordered_set>
#include "Config.h"
#include "MengeCore\BFSM\Goals\GoalAABB.h"

using Menge::BFSM::Goal;
using Menge::BFSM::AABBGoal;
using Menge::BFSM::AABBGoalFactory;

namespace Evacuation
{
	class EVACUATION_API EvacuationAABBGoal : public AABBGoal
	{
	public:
		static const std::string NAME;

		virtual std::string getStringId() const { return NAME; }

		std::unordered_set<std::string> _adjacent;
	};

	class EVACUATION_API EvacuationAABBGoalFactory : public AABBGoalFactory
	{
	public:
		virtual const char * name() const;

	protected:
		Goal * instance() const;

		virtual bool setFromXML(Goal * goal, TiXmlElement * node, const std::string & behaveFldr) const;
	};
}

#endif // !__EVACUATION_GOAL_FACTORY_H__
