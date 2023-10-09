#include <stdio.h>

extern int y;
void Func2(void);
void Func3(void);
int x = 9;
void main2(void)
{
	Func3();
}
void Func1()
{
	printf("f1: x=%d\ty=%d",x,y);
}