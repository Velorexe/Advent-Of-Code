#ifndef ADVENT_DAY3
#define ADVENT_DAY3

#include <string>

namespace advent_day3
{
	std::int32_t calcScore(char item) {
		if (std::isupper(item)) {
			return int(item) - 38;
		}

		return int(item) - 96;
	}

	std::set<char> getCommonChars(std::string s1, std::string s2) {
		std::map<char, int> s1Score;
		std::set<char> overlap;

		for (int i = 0; i < s1.length(); i++) {
			s1Score[s1[i]]++;
		}

		for (int i = 0; i < s2.length(); i++) {
			if (s1Score[s2[i]] > 0) {
				overlap.insert(s2[i]);
			}
		}

		return overlap;
	}

	// Task 1
	std::int32_t calcIndividuals(std::vector<std::string> rucksacks) {
		std::int32_t totalScore = 0;

		for (int i = 0; i < rucksacks.size(); i++) {
			std::int32_t halved = rucksacks.at(i).length() / 2;

			std::string halves[2] = { rucksacks.at(i).substr(0, halved), rucksacks.at(i).substr(halved, halved) };

			std::set<char> overlapSet = getCommonChars(halves[0], halves[1]);
			std::vector<char> overlap(overlapSet.begin(), overlapSet.end());

			std::int32_t highestScore = -1;
			for (int j = 0; j < overlap.size(); j++) {
				std::int32_t score = calcScore(overlap.at(j));
				if (score > highestScore) {
					highestScore = score;
				}
			}

			totalScore += highestScore;
		}

		return totalScore;
	}

	// Task 2
	std::int32_t calcGroups(std::vector<std::vector<std::string>> groups) {
		std::int32_t totalScore = 0;

		for (int i = 0; i < groups.size(); i++) {
			std::vector<std::string> currentGroup = groups.at(i);
			std::set<char> overlapSet = getCommonChars(currentGroup.at(0), currentGroup.at(1));

			overlapSet = getCommonChars(std::string(overlapSet.begin(), overlapSet.end()), currentGroup.at(2));
			std::vector<char> overlap(overlapSet.begin(), overlapSet.end());

			std::int32_t highestScore = -1;
			for (int j = 0; j < overlap.size(); j++) {
				std::int32_t score = calcScore(overlap.at(j));
				if (score > highestScore) {
					highestScore = score;
				}
			}

			totalScore += highestScore;
		}

		return totalScore;
	}

	void run()
	{
		std::vector<std::string> rucksacks;
		boost::split_regex(rucksacks, advent_import::import_advent(3), boost::regex("\n"));

		std::vector<std::vector<std::string>> groups;

		for (int i = 0; i < rucksacks.size() / 3; i++) {
			groups.push_back({ rucksacks.at(i * 3), rucksacks.at(i * 3 + 1), rucksacks.at(i * 3 + 2) });
		}

		std::cout << "Score for all rucksacks: " << calcIndividuals(rucksacks) << '\n';
		std::cout << "Score for groups: " << calcGroups(groups);
	}
}

#endif
