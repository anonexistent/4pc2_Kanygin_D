// pz_013.cpp

#include <iostream>
#include "../pz_013Lib/Library.h"


int main()
{
    std::cout << "Hello World!\n";

    FibonacciInit(1, 1);
	do
	{
		std::cout << FibonacciIndex() << ": " << FibonacciCurrent() << std::endl;

	} while (FibonacciNext());

	std::cout << FibonacciIndex() + 1 << "end" << std::endl;
}