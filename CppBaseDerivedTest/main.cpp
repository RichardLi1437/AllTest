#include <iostream>
using namespace std;

class A {
public:
	virtual void f1() {
		cout << "A::f1()" << endl;
	}

	void f2() {
		cout << "A::f2()" << endl;
	}
};

class B : public A {
public:
	void f1() {
		cout << "B::f1()" << endl;
	}

	void f2() {
		cout << "B::f2()" << endl;
	}
};

void baseDerived() {
	A *pa = new A();
	B *pb = new B();
	A *pc = new B();
	pa->f1();
	pa->f2();
	pb->f1();
	pb->f2();
	pc->f1();
	pc->f2();
	/*
	expected result:
	A::f1()
	A::f2()
	B::f1()
	B::f2()
	B::f1()
	A::f2()
	*/
}

void toRadixM(int n, int m) {
	if (n < 0) {
		cout << "-";
		toRadixM(-n, m);
	} else if (n == 0) {
		return;
	} else {
		toRadixM(n/m, m);
		cout << n % m;
	}
}

void toRadixMEx(int n, int m) {
	if (n == 0) {
		cout << "0" << endl;
	} else { 
		toRadixM(n, m);
		cout << endl;
	}
}

void toRadixM2(int n, int m) {
	if (n == 0) {
		cout << "0" << endl;
		return;
	}
	int num[32], s = 0;
	if (n < 0) {
		cout << "-";
		n = -n;
	}
	while (n > 0) {
		num[s++] = n % m;
		n = n / m;
	}
	for (int i = s-1; i >= 0; i--) {
		cout << num[i];
	}
	cout << endl;
}

int main() {
	//baseDerived();
	//toRadixMEx(231, 7);
	//toRadixM2(231, 7);
	cout << '\0' << endl;
}