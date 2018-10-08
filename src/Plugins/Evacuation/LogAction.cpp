#include "LogAction.h"
#include "Simulator.h"

bool Evacuation::LogActionFactory::_is_logger_file_set = false;

void Evacuation::LogAction::onEnter(Menge::Agents::BaseAgent * agent)
{
	SIM_LOGGER << SimLogger::INFO_MSG << _text;
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
