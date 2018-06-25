/*!
 *	@file		SamplePlugin.cpp  
 *	@brief		Plugin for extended elements.
 */

#include "EvacuationConfig.h"
#include "MengeCore/PluginEngine/CorePluginEngine.h"

using Menge::PluginEngine::CorePluginEngine;

/*!
 *	@brief		Retrieves the name of the plug-in.
 *
 *	@returns	The name of the plug in.
 */
extern "C" EVACUATION_API const char * getName() {
	return "evacuation";
}

/*!
 *	@brief		Description of the plug-in.
 *
 *	@returns	A description of the plugin.
 */
extern "C" EVACUATION_API const char * getDescription() {
	return	"Utilities for simulating evacuation";
}

/*!
 *	@brief		Registers the plug-in with the PluginEngine
 *
 *	@param		engine		A pointer to the plugin engine.
 */
extern "C" EVACUATION_API void registerPlugin(CorePluginEngine * engine ) {
	//engine->registerGoalSelectorFactory();
}

