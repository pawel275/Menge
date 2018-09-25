#include "FollowCondition.h"
#include "Agent.h"
#include "MengeCore\Core.h"
#include "MengeCore\Agents\SimulatorInterface.h"

namespace Evacuation {
	FollowCondition::FollowCondition() : Condition(), _group(0.f)
	{
	}

	FollowCondition::FollowCondition(const FollowCondition & cond) : Condition(cond)
	{
		_group = cond._group;
	}

	bool FollowCondition::conditionMet(Agents::BaseAgent * agent, const Goal * goal)
	{
		if (agent->_class == _group)
		{
			return true;
		}

		for each (Agents::NearAgent nearAgt in agent->_nearAgents)
		{
			if (nearAgt.agent->_class == _group && SIMULATOR->queryVisibility(agent->_pos, nearAgt.agent->_pos))
			{
				return true;
			}
		}

		return false;
	}

	Condition * FollowCondition::copy()
	{
		return new FollowCondition(*this);
	}

	FollowCondFactory::FollowCondFactory()
	{
		_groupID = _attrSet.addSizeTAttribute("group", false, 0.f);
	}

	bool FollowCondFactory::setFromXML(Condition * condition, TiXmlElement * node, const std::string & behaveFldr) const
	{
		FollowCondition * fCond = dynamic_cast<FollowCondition *>(condition);
		assert(fCond != 0x0 &&
			"Trying to set the properties of a follow condition on an incompatible object");

		if (!ConditionFactory::setFromXML(fCond, node, behaveFldr)) return false;

		fCond->setGroup(_attrSet.getSizeT(_groupID));
		return true;
	}
}