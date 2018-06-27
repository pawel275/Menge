#include "GoalFactory.h"
#include "MengeCore\PluginEngine\Attribute.h"

const std::string Evacuation::EvacuationAABBGoal::NAME = "evAABB";

const char * Evacuation::EvacuationAABBGoalFactory::name() const
{
	return EvacuationAABBGoal::NAME.c_str();
}

Goal * Evacuation::EvacuationAABBGoalFactory::instance() const
{
	return new EvacuationAABBGoal();
}

bool Evacuation::EvacuationAABBGoalFactory::setFromXML(Goal * goal, TiXmlElement * node, const std::string & behaveFldr) const
{
	if (!AABBGoalFactory::setFromXML(goal, node, behaveFldr))
	{
		return false;
	}

	EvacuationAABBGoal * aabbGoal = (EvacuationAABBGoal*)goal;
	Menge::StringAttribute adjacentAttr("adjacent", false, "");

	if (!adjacentAttr.extract(node))
	{
		return false;
	}

	std::stringstream ss(adjacentAttr.getString());
	size_t adj;
	while (ss >> adj)
	{
		aabbGoal->_adjacent.push_back(adj);
		if (ss.peek() == ',')
			ss.ignore();
	}

	return !aabbGoal->_adjacent.empty();
}