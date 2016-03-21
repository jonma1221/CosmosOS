// Version3.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"
#include <iostream>
#include <string>
#include <sstream>
#include <ctime>
#include <cstdlib>
#include "matrix.h"

using namespace std;

int main()
{
	srand(time(0));
	int rowMatrix1, colMatrix1;
	int rowMatrix2, colMatrix2;
	

	cout << "[Matrix 1] Enter row: ";
	cin >> rowMatrix1;
	cout << "[Matrix 1] Enter col: ";
	cin >> colMatrix1;
	cout << "[Matrix 2] Enter row: ";
	cin >> rowMatrix2;
	cout << "[Matrix 2] Enter col: ";
	cin >> colMatrix2;

	matrix matrix1(rowMatrix1, colMatrix1);
	matrix1.fillMatrix();
	cout << "Matrix 1:";
	matrix1.print();

	matrix matrix2(rowMatrix2, colMatrix2);
	matrix2.fillMatrix();
	cout << "Matrix 2:";
	matrix2.print();

	int choice = 0;
	while (choice != 4) {
		cout << "\nMenu: " << endl;
		cout << "1 : Add " << endl;
		cout << "2 : Subtract" << endl;
		cout << "3 : Multipliy" << endl;
		cout << "4 : Exit" << endl;
		cin >> choice;

		if (choice == 1) {
			if (rowMatrix1 != rowMatrix2 || colMatrix1 != colMatrix2)
			{
				cout << "Error: Different dimensions\n";
				break;
			}
			matrix sum(rowMatrix1, colMatrix1);
			sum = matrix1 + matrix2;
			sum.print();
			cout << endl;
		}
		else if (choice == 2) {
			if (rowMatrix1 != rowMatrix2 || colMatrix1 != colMatrix2)
			{
				cout << "Error: Different dimensions\n";
				break;
			}
			matrix difference(rowMatrix1, colMatrix1);
			difference = matrix1 - matrix2;
			difference.print();
			cout << endl;
		}
		else if (choice == 3) {
			if (colMatrix1 != rowMatrix2)
			{
				cout << "Error: Different dimensions\n";
				break;
			}
			matrix product(rowMatrix1, colMatrix2);
			product = matrix1 * matrix2;
			product.print();
			cout << endl;
		}
		else {
			break;
		}
	} 
	getchar();
}


