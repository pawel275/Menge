#ifndef __EVACUATION_UNKNOWN_PATH_GOAL_SELECTOR_H__
#define __EVACUATION_UNKNOWN_PATH_GOAL_SELECTOR_H__

#include "Config.h"
#include "MengeCore/BFSM/GoalSelectors/GoalSelectorSet.h"

namespace Evacuation
{
	using Menge::Agents::BaseAgent;
	using Menge::BFSM::Goal;
	using Menge::BFSM::GoalSelector;

	class EVACUATION_API UnknownPathGoalSelector : public Menge::BFSM::SetGoalSelector
	{
	public:

		virtual Goal * getGoal(const BaseAgent * agent) const override;
	};

	class EVACUATION_API UnknownPathGoalSelectorFactory : public Menge::BFSM::SetGoalSelectorFactory {
	public:

		virtual const char * name() const { return "unknown_path"; }

		virtual const char * description() const {
			return  "A goal selector for agents who don't know the path to exit";
		};

	protected:

		GoalSelector * instance() const { return new UnknownPathGoalSelector(); }
	};
}

#endif