#ifndef __EVACUATION_KNOWN_PATH_GOAL_SELECTOR_H__
#define __EVACUATION_KNOWN_PATH_GOAL_SELECTOR_H__

#include "Config.h"
#include "MengeCore/BFSM/GoalSelectors/GoalSelectorSet.h"

namespace Evacuation
{
	using Menge::Agents::BaseAgent;
	using Menge::BFSM::Goal;
	using Menge::BFSM::GoalSelector;

	class EVACUATION_API KnownPathGoalSelector : public Menge::BFSM::SetGoalSelector
	{
	public:

		virtual Goal * getGoal(const BaseAgent * agent) const override;
	};

	class EVACUATION_API KnownPathGoalSelectorFactory : public Menge::BFSM::SetGoalSelectorFactory {
	public:

		virtual const char * name() const { return "known_path"; }

		virtual const char * description() const {
			return  "A goal selector for agents who know the path to exit";
		};

	protected:

		GoalSelector * instance() const { return new KnownPathGoalSelector(); }
	};
}

#endif