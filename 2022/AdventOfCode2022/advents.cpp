#include <iostream>
#include <string>

#include "advent_day1.h";
#include "advent_day2.h";

int main()
{
	void (*adventFunctions[])() = { advent_day1::run, advent_day2::run };

	char advent[sizeof(adventFunctions)];
	std::cout << "Pick an advent between 1 and " << sizeof(adventFunctions) << '\n';

	std::cin.getline(advent, sizeof(adventFunctions));

	std::int32_t pickedAdvent = atoi(advent);

	adventFunctions[pickedAdvent - 1]();
}