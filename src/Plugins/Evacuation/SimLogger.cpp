#include "SimLogger.h"

Evacuation::SimLogger Evacuation::SIM_LOGGER;

void Evacuation::SimLogger::writeHeader()
{
	_file << "<html>\n";
	_file << "<head>\n";
	_file << "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\n";
	_file << "<title>Simulation Log</title>\n";
	_file << "<link rel=\"stylesheet\" type=\"text/css\" href=\"../log.css\" ";
	_file << "media=\"screen\"/>\n";
	_file << "</head>\n\n";

	_file << "<body>\n";
	_file << "<div id=\"content\">\n";
	_file << "<h1>Simulation Log</h1>\n";
	_file << "<div class=\"box\">\n";
	_file << "<table>\n";
}

void Evacuation::SimLogger::writeTail()
{
	if (_streamType != UNDEF_LOG) {
		_file << "</td>\n\t</tr>\n";
	}
	_file << "</table>\n";
	_file << "</div>\n";
	_file << "</div>\n";
	_file << "</body>\n";
	_file << "</html>\n";
}