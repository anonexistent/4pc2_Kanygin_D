// MathLibrary.cpp
#include "pch.h"
#include <utility>
#include <limits.h>
#include "Library.h"

#pragma region consts
static unsigned long long previous_; // last value
static unsigned long long current_; // cur value
static unsigned index_; // cur position
#pragma endregion

/// <summary>
/// Initialization!!!
/// </summary>
/// <param name="const current"></param>
/// <param name="const previous"></param>
void fibonacci_init(const unsigned long long a, const unsigned long long b)
{
	index_ = 0;
	current_ = a;
	previous_ = b;
}
// work next
bool fibonacci_next()
{
	// so much
	if ((ULLONG_MAX - previous_ < current_) || (UINT_MAX == index_)) return false;
	// if i==0 => b
	if (index_ > 0) previous_ += current_;

	std::swap(current_, previous_);
	++index_;
	return true;
}
// get cur value
unsigned long long fibonacci_current()
{
	return current_;
}
// get cur value index
unsigned fibonacci_index()
{
	return index_;
}