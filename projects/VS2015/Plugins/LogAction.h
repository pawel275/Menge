#ifndef __LOG_ACTION_H__
#define __LOG_ACTION_H__

#include "MengeCore/BFSM/Actions/Action.h"
#include "MengeCore/BFSM/Actions/ActionFactory.h"

using namespace Menge::BFSM;

namespace Evacuation 
{
	class TeleportActFactory;

	class LogAction : public Action 
	{
	public:
		LogAction();

		virtual ~LogAction();

		virtual void onEnter(Menge::Agents::BaseAgent * agent);

		friend class LogActionFactory;
	};

	class  LogActionFactory : public ActionFactory 
	{
	public:
		LogActionFactory();

		virtual const char * name() const { return "log"; }

		virtual const char * description() const {
			return "Simulation log action";
		};

	protected:
		Action * instance() const;

		virtual bool setFromXML(Action * action, TiXmlElement * node,
			const std::string & behaveFldr) const;
	};

}
#endif // !__LOG_ACTION_H__
