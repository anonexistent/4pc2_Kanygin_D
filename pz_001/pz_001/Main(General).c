#include <stdio.h>

extern int x;
void Func1(void);
int y = 21;
void Func2()
{
	printf("f2: y=%d", y);
}
int z = 29;
void Func3()
{
	Func1();
	printf("f3: x=%d\ty=%d\z=%d",x,y,z);
}