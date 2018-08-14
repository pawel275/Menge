#ifndef __FOLLOW_CONDITION_H__
#define __FOLLOW_CONDITION_H__

#include "Config.h"
#include "MengeCore/BFSM/Transitions/Condition.h"
#include "MengeCore/BFSM/Transitions/ConditionFactory.h"

using namespace Menge;
using namespace Menge::BFSM;
namespace Evacuation {
	class EVACUATION_API FollowCondition : public Condition {
	public:

		FollowCondition();

		FollowCondition(const FollowCondition & cond);

		virtual bool conditionMet(Agents::BaseAgent * agent, const Goal * goal);

		virtual Condition * copy();

		void setGroup(size_t dist) { _group = dist * dist; }

	protected:

		size_t	_group;
	};

	class EVACUATION_API FollowCondFactory : public ConditionFactory {
	public:

		FollowCondFactory();

		virtual const char * name() const { return "follow"; }

		virtual const char * description() const {
			return "The follow condition.  It becomes active when an agent is in range of another agent who knows the path";
		}

	protected:

		virtual Condition * instance() const { return new FollowCondition(); }

		virtual bool setFromXML(Condition * condition, TiXmlElement * node,
			const std::string & behaveFldr) const;

		size_t	_groupID;
	};
}
#endif // __FOLLOW_CONDITION_H__
