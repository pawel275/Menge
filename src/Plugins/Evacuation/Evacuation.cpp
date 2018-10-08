/*!
 *	@file		Evacuation.cpp
 *	@brief		Plugin for simulating evacuation.
 */

#include "Config.h"
#include "DBEntry.h"
#include "UnknownPathGoalSelector.h"
#include "KnownPathGoalSelector.h"
#include "GoalFactory.h"
#include "GoalRenderer.h"
#include "AgentGenerator.h"
#include "FollowCondition.h"
#include "LogAction.h"
#include "MengeCore/PluginEngine/CorePluginEngine.h"
#include "MengeVis/PluginEngine/VisPluginEngine.h"

extern "C" {
	/*!
	*	@brief		Retrieves the name of the plug-in.
	*
	*	@returns	The name of the plug in.
	*/
	EVACUATION_API const char * getName() {
		return "evacuation";
	}

	/*!
	*	@brief		Description of the plug-in.
	*
	*	@returns	A description of the plugin.
	*/
	EVACUATION_API const char * getDescription() {
		return	"Utilities for simulating evacuation";
	}

	/*!
	*	@brief		Registers the plug-in with the PluginEngine
	*
	*	@param		engine		A pointer to the plugin engine.
	*/
	EVACUATION_API void registerCorePlugin(Menge::PluginEngine::CorePluginEngine * engine) {
		engine->registerModelDBEntry(new Evacuation::DBEntry());
		engine->registerGoalFactory(new Evacuation::EvacuationAABBGoalFactory());
		engine->registerGoalSelectorFactory(new Evacuation::UnknownPathGoalSelectorFactory());
		engine->registerGoalSelectorFactory(new Evacuation::KnownPathGoalSelectorFactory());
		engine->registerAgentGeneratorFactory(new Evacuation::AgentGeneratorFactory());
		engine->registerConditionFactory(new Evacuation::FollowCondFactory());
		engine->registerActionFactory(new Evacuation::LogActionFactory());
	}

	EVACUATION_API void registerVisPlugin(MengeVis::PluginEngine::VisPluginEngine * engine) {
		engine->registerGoalRenderer(new Evacuation::GoalRenderer());
	}
}