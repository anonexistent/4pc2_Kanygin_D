#pragma once
// - funcs
#pragma once

#ifdef pz_013Lib_EXPORTS
#define MathLibrary_Api __declspec(dllexport)
#else
#define MathLibrary_Api __declspec(dllimport)
#endif // pz_013Lib_exports

extern "C" MathLibrary_Api void FibonacciInit(
	const unsigned long long a, const unsigned long long b);
extern "C" MathLibrary_Api bool FibonacciNext();
extern "C" MathLibrary_Api unsigned long long FibonacciCurrent();
extern "C" MathLibrary_Api unsigned FibonacciIndex();