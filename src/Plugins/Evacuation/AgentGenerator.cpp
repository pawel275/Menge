#include "AgentGenerator.h"
#include "Agent.h"

namespace Evacuation {
	EvacuationAgentGenerator::EvacuationAgentGenerator() : ExplicitGenerator() {
	}

	void EvacuationAgentGenerator::setAgentPosition(size_t i, BaseAgent * agt) {
		if (i >= _positions.size()) {
			throw AgentGeneratorFatalException("ExplicitGenerator trying to access an agent "
				"outside of the specified population");
		}
		agt->_pos = addNoise(_positions[i]);

		Agent *agent = dynamic_cast<Agent*>(agt);
		agent->_start_goal_id = _goals[i];
	}

	void EvacuationAgentGenerator::addGoal(const size_t g)
	{
		_goals.push_back(g);
	}

	bool AgentGeneratorFactory::setFromXML(AgentGenerator * gen, TiXmlElement * node,
		const std::string & specFldr) const {
		EvacuationAgentGenerator * eGen = dynamic_cast<EvacuationAgentGenerator *>(gen);
		assert(eGen != 0x0 &&
			"Trying to set attributes of an explicit agent generator component on an "
			"incompatible object");

		if (!Menge::Agents::AgentGeneratorFactory::setFromXML(eGen, node, specFldr)) return false;

		for (TiXmlElement * child = node->FirstChildElement();
			child;
			child = child->NextSiblingElement()) {
			if (child->ValueStr() == "Agent") {
				try {
					Vector2 p = parseAgent(child);
					size_t g = parseAgentGoal(child);
					eGen->addPosition(p);
					eGen->addGoal(g);
				}
				catch (AgentGeneratorException) {
					return false;
				}
			}
			else {
				logger << Logger::WARN_MSG;
				logger << "Found an unexpected child tag in an AgentGroup on line ";
				logger << node->Row() << ".  Ignoring the tag: " << child->ValueStr() << ".";
			}
		}

		return true;
	}

	size_t AgentGeneratorFactory::parseAgentGoal(TiXmlElement * node) const
	{
		size_t goal;
		int sGoal;
		bool valid = true;
		if (node->Attribute("goal", &sGoal)) {
			goal = (size_t)sGoal;
		}
		else {
			valid = false;
		}
		if (!valid) {
			Menge::logger << Menge::Logger::ERR_MSG << "Agent on line " << node->Row();
			Menge::logger << " didn't define goal!";
			throw Menge::Agents::AgentGeneratorFatalException("Agent in evacuation generator didn't "
				"define a goal");
		}
		return goal;
	}
}