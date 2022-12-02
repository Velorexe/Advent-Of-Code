#ifndef ADVENT_DAY2
#define ADVENT_DAY2

#include <vector> 
#include "advent_import.h"

namespace advent_day2
{
	int findIndex(std::vector<std::int32_t> arr, int32_t value) {
		for (int i = 0; i < arr.size(); i++) {
			if (arr.at(i) == value) {
				return i;
			}
		}

		return -1;
	}

	void run()
	{
		std::vector<std::string> rounds;
		boost::split_regex(rounds, advent_import::import_advent(2), boost::regex("\n"));

		std::vector<std::vector<int>> rpsLookup = { {3, 6, 0}, {0, 3, 6}, {6, 0, 3} };

		std::map<std::string, int32_t> movePointLookup = { {"A", 1}, {"B", 2}, {"C", 3}, { "X", 1}, {"Y", 2}, {"Z", 3} };
		std::string mapper[3] = { "A", "B", "C" };

		std::int32_t totalScore = 0;

		for (int i = 0; i < rounds.size(); i++) {
			std::vector<std::string> round;
			boost::split(round, rounds.at(i), boost::is_any_of(" "));

			// Lose
			if (round.at(1) == "X") {
				totalScore += 0;

				std::vector<std::int32_t> arr = rpsLookup[movePointLookup.at(round.at(0)) - 1];
				std::int32_t index = advent_day2::findIndex(arr, 0);

				totalScore += movePointLookup[mapper[index]];
			}
			// Draw
			else if (round.at(1) == "Y") {
				totalScore += 3;

				std::vector<std::int32_t> arr = rpsLookup[movePointLookup.at(round.at(0)) - 1];
				std::int32_t index = advent_day2::findIndex(arr, 3);

				totalScore += movePointLookup[mapper[index]];
			}
			// WIN
			else if (round.at(1) == "Z") {
				totalScore += 6;

				std::vector<std::int32_t> arr = rpsLookup[movePointLookup.at(round.at(0)) - 1];
				std::int32_t index = advent_day2::findIndex(arr, 6);

				totalScore += movePointLookup[mapper[index]];
			}
		}


		std::cout << "Totalscore: " << totalScore;
	}
}

#endif
