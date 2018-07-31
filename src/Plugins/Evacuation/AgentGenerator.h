#ifndef __EVACUATION_AGENT_GENERATOR__
#define __EVACUATION_AGENT_GENERATOR__

#include "Menge\MengeCore\Math\Vector2.h"
#include "Menge\MengeCore\Agents\BaseAgent.h"
#include "Menge\MengeCore\Agents\AgentGenerators\AgentGenerator.h"
#include "Menge\MengeCore\Agents\AgentGenerators\ExplicitAgentGenerator.h"

using namespace Menge::Math;

namespace Evacuation {
	class AgentGenerator : public Menge::Agents::ExplicitGenerator {
	public:

		AgentGenerator();

		virtual void setAgentPosition(size_t i, Menge::Agents::BaseAgent * agt);

		void addGoal(const size_t p);

	protected:

		std::vector<size_t>	_goals;
	};

	class AgentGeneratorFactory : public Menge::Agents::ExplicitGeneratorFactory {
	public:

		virtual const char * name() const { return "evacuation"; }

		virtual const char * description() const {
			return "Agent generation is done via an explicit list of agent positions and goals, given "
				"in the XML specification.";
		};

	protected:

		AgentGenerator * instance() const { return new AgentGenerator(); }

		virtual bool setFromXML(AgentGenerator * gen, TiXmlElement * node,
			const std::string & behaveFldr) const;

		size_t parseAgentGoal(TiXmlElement * node) const;
	};
}

#endif // !__EVACUATION_AGENT_GENERATOR__
