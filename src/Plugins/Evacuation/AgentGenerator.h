#ifndef __EVACUATION_AGENT_GENERATOR__
#define __EVACUATION_AGENT_GENERATOR__

#include "Config.h"
#include "Menge\MengeCore\Math\Vector2.h"
#include "Menge\MengeCore\Agents\BaseAgent.h"
#include "Menge\MengeCore\Agents\AgentGenerators\AgentGenerator.h"
#include "Menge\MengeCore\Agents\AgentGenerators\ExplicitAgentGenerator.h"

#include "MengeCore/mengeCommon.h"
#include "MengeCore/Agents/AgentGenerators/AgentGenerator.h"
#include "MengeCore/Agents/AgentGenerators/AgentGeneratorFactory.h"

#include <vector>

using namespace Menge;
using namespace Menge::Agents;

namespace Evacuation {
	class EVACUATION_API EvacuationAgentGenerator : public ExplicitGenerator {
	public:

		EvacuationAgentGenerator();

		virtual void setAgentPosition(size_t i, BaseAgent * agt);

		void addGoal(const size_t g);

	protected:

		std::vector<size_t> _goals;
	};

	class EVACUATION_API AgentGeneratorFactory : public Menge::Agents::ExplicitGeneratorFactory {
	public:

		virtual const char * name() const { return "evacuation"; }

		virtual const char * description() const {
			return "Agent generation is done via an explicit list of agent positions and goals, given "
				"in the XML specification.";
		};

	protected:

		AgentGenerator * instance() const { return new EvacuationAgentGenerator(); }

		virtual bool setFromXML(AgentGenerator * gen, TiXmlElement * node,
			const std::string & behaveFldr) const;

		size_t parseAgentGoal(TiXmlElement * node) const;
	};
}
#endif // !__EVACUATION_AGENT_GENERATOR__
