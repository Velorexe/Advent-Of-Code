#ifndef ADVENT_DAY1
#define ADVENT_DAY1

#include <iostream>
#include <string>
#include <vector>
#include <boost/algorithm/string_regex.hpp>
#include <fstream>
#include "advent_import.h"

namespace advent_day1
{
	void run()
	{
		std::vector<std::string> unorderedElfs;

		boost::split_regex(unorderedElfs, advent_import::import_advent(1), boost::regex("\n\n"));

		std::vector<std::int32_t> caloriesOverview(unorderedElfs.size());

		for (int i = 0; i < unorderedElfs.size(); i++) {
			std::vector<std::string> rawCalories;
			boost::split(rawCalories, unorderedElfs.at(i), boost::is_any_of("\n"));

			std::int32_t calories = 0;

			for (int j = 0; j < rawCalories.size(); j++) {
				calories += atoi(rawCalories.at(j).c_str());
			}

			caloriesOverview.push_back(calories);
		}

		std::sort(caloriesOverview.begin(), caloriesOverview.end());

		std::int32_t topCalories = caloriesOverview.at(caloriesOverview.size() - 1) + caloriesOverview.at(caloriesOverview.size() - 2) + caloriesOverview.at(caloriesOverview.size() - 3);

		std::cout << "Highest amount of calories: " << caloriesOverview.at(caloriesOverview.size() - 1) << '\n';
		std::cout << "Total of top 3: " << topCalories;
	}
}

#endif