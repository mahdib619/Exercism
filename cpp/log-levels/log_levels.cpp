#include <string>

#define MESSAGE_INDEX_OFFSET 3
#define LEVEL_MESSAGE_SPLITTER "]: "

using namespace std;

namespace log_line
{
	string message(string log)
	{
		int message_index = log.find(LEVEL_MESSAGE_SPLITTER) + MESSAGE_INDEX_OFFSET;
		return log.substr(message_index);
	}
	string log_level(string log)
	{
		int level_last_index = log.find(LEVEL_MESSAGE_SPLITTER) - 1;
		return log.substr(1, level_last_index);
	}
	string reformat(string log)
	{
		return message(log) + " (" + log_level(log) + ")";
	}
}
