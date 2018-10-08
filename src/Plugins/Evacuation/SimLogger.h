#ifndef __SIM_LOGGER_H__
#define __SIM_LOGGER_H__

#include "MengeCore\Runtime\Logger.h"
#include "Config.h"

namespace Evacuation
{
	class SimLogger : public Menge::Logger
	{
	public:
		~SimLogger()
		{
			close();
		}

	protected:
		void writeTail() override;

		void writeHeader() override;
	};

	extern SimLogger SIM_LOGGER;
}

#endif // !__SIM_LOGGER_H__
