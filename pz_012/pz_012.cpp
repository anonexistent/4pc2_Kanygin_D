// pz_012.cpp

#include <iostream>
#include "../pz_012Lib/Library.h"

int main()
{
    std::cout << "Hello World!\n";

    std::cout << "1+2 = ";
    std::cout << pz_012Lib::Arithmetic::Add(1, 2);
    std::cout << "\n2*2 = ";
    std::cout << pz_012Lib::Arithmetic::Multiply(2, 2);
    std::cout << "\n10-9.1 = ";
    std::cout << pz_012Lib::Arithmetic::Divide(10, 9.1);
    std::cout << "\n10/9.1 = ";
    std::cout << pz_012Lib::Arithmetic::Subtract(10, 9.1);
    std::cout << "\n10**3.5 = ";
    std::cout << pz_012Lib::Arithmetic::Pow(10, 3.5);
    std::cout << "\n∛9 = ";
    std::cout << pz_012Lib::Arithmetic::InversePow(9,3);
}