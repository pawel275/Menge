#ifndef __EVACUATION_GOAL_RENDERER_H__
#define __EVACUATION_GOAL_RENDERER_H__

#include "Config.h"
#include "GoalFactory.h"
#include "MengeVis/Runtime/GoalRenderer/AABBGoalRenderer.h"

namespace Evacuation
{
	class EVACUATION_API GoalRenderer : public MengeVis::Runtime::GoalVis::AABBGoalRenderer
	{
	public:
		virtual std::string getElementName() const { return EvacuationAABBGoal::NAME; };
	};
}
#endif	// __EVACUATION_GOAL_RENDERER_H__
