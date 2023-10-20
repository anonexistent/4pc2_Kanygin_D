#include "pch.h"
#include "Library.h"
#include "cmath"

namespace pz_012Lib
{
	double Arithmetic::Add(double a, double b)
	{
		return a + b;
	}
	double Arithmetic::Subtract(double a, double b)
	{
		return a - b;
	}
	double Arithmetic::Multiply(double a, double b)
	{
		return a * b;
	}
	double Arithmetic::Divide(double a, double b)
	{
		return a / b;
	}
	double Arithmetic::Pow(double a, double b)
	{
		return pow(a, b);
	}
	double Arithmetic::InversePow(double a, double b)
	{
		return pow(a, 1 / b);
	}
}