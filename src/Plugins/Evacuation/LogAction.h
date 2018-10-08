#ifndef __LOG_ACTION_H__
#define __LOG_ACTION_H__

#include "SimLogger.h"
#include "MengeCore\BFSM\Actions\Action.h"
#include "MengeCore\BFSM\Actions\ActionFactory.h"

using namespace Menge::BFSM;

namespace Evacuation
{
	class LogAction : public Action {
	public:
		virtual void onEnter(Menge::Agents::BaseAgent * agent);

		virtual void setText(std::string text);

	private:
		std::string _text;
	};

	class LogActionFactory : public ActionFactory {
	public:
		LogActionFactory();

		virtual const char * name() const { return "log"; }

		virtual const char * description() const { return "Simulation log action"; };

	protected:
		Action * instance() const { return new LogAction(); };

		virtual bool setFromXML(Action * action, TiXmlElement * node,
			const std::string & behaveFldr) const;

		size_t _text_id;

		static bool _is_logger_file_set;
	};
}
#endif // !__LOG_ACTION_H__
