#ifndef ADVENT_IMPORT
#define ADVENT_IMPORT

#include <string>
#include <fstream>
#include <fmt/format.h>

namespace advent_import {
	std::string import_advent(std::int32_t day) {
		std::ifstream file(fmt::format("advent_day{}_input.txt", day));
		return std::string((std::istreambuf_iterator<char>(file)), std::istreambuf_iterator<char>());
	}
}

#endif