#include "LogAction.h"
#include "Simulator.h"
#include "Agent.h"
#include "MengeCore\Agents\SimulatorInterface.h"

inline void findAndReplace(std::string & source, const std::string & find,
	const std::string & replace)
{
	size_t fLen = find.size();
	size_t rLen = replace.size();
	for (size_t pos = 0;
		(pos = source.find(find, pos)) != std::string::npos;
		pos += rLen) {
		source.replace(pos, fLen, replace);
	}
}

bool Evacuation::LogActionFactory::_is_logger_file_set = false;

void Evacuation::LogAction::onEnter(Menge::Agents::BaseAgent * agent)
{
	Agent * agt = dynamic_cast<Agent *>(agent);
	
	std::string processed_text(_text);
	findAndReplace(processed_text, "{class}", std::to_string(agt->_class));
	findAndReplace(processed_text, "{time}", std::to_string(Menge::SIMULATOR->getGlobalTime()));

	SIM_LOGGER << SimLogger::INFO_MSG << processed_text;
}

void Evacuation::LogAction::setText(std::string text)
{
	_text = text;
}

Evacuation::LogActionFactory::LogActionFactory()
{
	_text_id = _attrSet.addStringAttribute("text", true);
}

bool Evacuation::LogActionFactory::setFromXML(Action * action, TiXmlElement * node, const std::string & behaveFldr) const
{
	LogAction * logAction = dynamic_cast<LogAction *>(action);

	if (!ActionFactory::setFromXML(action, node, behaveFldr)) return false;

	logAction->setText(_attrSet.getString(_text_id));
	
	if (!_is_logger_file_set)
	{
		SIM_LOGGER.setFile(behaveFldr + "\\simLog.html");
		_is_logger_file_set = true;
	}

	return true;
}
