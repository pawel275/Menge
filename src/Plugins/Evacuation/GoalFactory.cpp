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
	Menge::StringAttribute adjacentAttr("adjacent", true, "");

	if (!adjacentAttr.extract(node))
	{
		return false;
	}

	std::stringstream ss(adjacentAttr.getString());
	while (ss.good())
	{
		std::string adj;
		std::getline(ss, adj, ',');
		aabbGoal->_adjacent.insert(adj);
	}

	return !aabbGoal->_adjacent.empty();
}