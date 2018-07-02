/*!
 *	@file		Evacuation.cpp
 *	@brief		Plugin for simulating evacuation.
 */

#include "Config.h"
#include "DBEntry.h"
#include "UnknownPathGoalSelector.h"
#include "KnownPathGoalSelector.h"
#include "GoalFactory.h"
#include "MengeCore/PluginEngine/CorePluginEngine.h"

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
		std::cout << "REGISTERING EVACUATION PLUGIN" << std::endl;
		engine->registerModelDBEntry(new Evacuation::DBEntry());
		engine->registerGoalFactory(new Evacuation::EvacuationAABBGoalFactory());
		engine->registerGoalSelectorFactory(new Evacuation::UnknownPathGoalSelectorFactory());
		engine->registerGoalSelectorFactory(new Evacuation::KnownPathGoalSelectorFactory());
	}
}