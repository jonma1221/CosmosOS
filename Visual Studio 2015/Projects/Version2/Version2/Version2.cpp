// Version2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <string>
#include <sstream>
#include <ctime>
#include <cstdlib>

using namespace std;

void print(double **matrix, int row, int col);
void fillMatrix(double **matrix, int row, int col);
void multiplication(double **matrix1, double **matrix2, int row1, int col1, int row2, int col2);
void addition(double **matrix1, double **matrix2, int row1, int col1, int row2, int col2);
void subtraction(double **matrix1, double **matrix2, int row1, int col1, int row2, int col2);

int main() {
	int totalRowMatrix1, totalColMatrix1;
	int totalRowMatrix2, totalColMatrix2;

	cout << "[Matrix 1] Enter row: ";
	cin >> totalRowMatrix1;
	cout << "[Matrix 1] Enter col: ";
	cin >> totalColMatrix1;
	cout << "[Matrix 2] Enter row: ";
	cin >> totalRowMatrix2;
	cout << "[Matrix 2] Enter col: ";
	cin >> totalColMatrix2;

	srand(time(0));
	double** matrix1 = new double*[totalRowMatrix1];
	for (int i = 0; i < totalRowMatrix1; ++i)
		matrix1[i] = new double[totalColMatrix1];
	fillMatrix(matrix1, totalRowMatrix1, totalColMatrix1);
	cout << "Matrix 1: \n";
	print(matrix1, totalRowMatrix1, totalColMatrix1);

	double** matrix2 = new double*[totalRowMatrix2];
	for (int i = 0; i < totalRowMatrix2; ++i)
		matrix2[i] = new double[totalColMatrix2];
	fillMatrix(matrix2, totalRowMatrix2, totalColMatrix2 );
	cout << "Matrix 2: \n";
	print(matrix2, totalRowMatrix2, totalColMatrix2 );
	
	int option = 0;
	while (option != 4) {
		cout << "\nEnter: " << endl;
		cout << "1: Add " << endl;
		cout << "2: Subtract " << endl;
		cout << "3: Multiply " << endl;
		cout << "4: Exit" << endl;    
		cin >> option;

		if (option == 1) {
			addition(matrix1, matrix2, totalRowMatrix1, totalColMatrix1, totalRowMatrix2, totalColMatrix2);
		}
		else if (option == 2) {
			subtraction(matrix1, matrix2, totalRowMatrix1, totalColMatrix1, totalRowMatrix2, totalColMatrix2);
		}
		else if (option == 3) {
			multiplication(matrix1, matrix2, totalRowMatrix1, totalColMatrix1, totalRowMatrix2, totalColMatrix2);
		}
		else {
			break;
		}
	} 
}

void fillMatrix(double **matrix, int row, int col)
{
	for (int i = 0; i < row; i++) {
		for (int j = 0; j < col; j++) {
			matrix[i][j] = rand() % 5;
		}
	}
}

void addition(double **matrix1, double **matrix2, int row1, int col1, int row2, int col2)
{
	double** sum = new double*[row1];
	if (row1 != row2 || col1 != col2)
	{
		cout << "Unable to add\n";
		return;
	}
	for (int i = 0; i < row1; i++)
		sum[i] = new double[col1];

	for (int i = 0; i < row1; i++) {
		for (int j = 0; j < col1; j++) {
			sum[i][j] = matrix1[i][j] + matrix2[i][j];
		}
	}
	print(sum, row1, col2);
}

void subtraction(double **matrix1, double **matrix2, int row1, int col1, int row2, int col2)
{
	double** difference = new double*[row1];
	if (row1 != row2 || col1 != col2)
	{
		cout << "Unable to subtract\n";
		return;
	}
	for (int i = 0; i < row1; ++i)
		difference[i] = new double[col1];

	for (int i = 0; i < row1; i++) {
		for (int j = 0; j < col1; j++) {
			difference[i][j] = matrix1[i][j] - matrix2[i][j];
		}
	}
	print(difference, row1, col2);
}

void multiplication(double **matrix1, double **matrix2, int row1, int col1, int row2, int col2)
{
	double** product = new double*[row1];
	if (col1 != row2)
	{
		cout << "Unable to multiply\n";
		return;
	}
	for (int i = 0; i < row1; ++i)
		product[i] = new double[col2] {};

	for (int i = 0; i < row1; i++) {
		for (int j = 0; j < col2; j++) {
			for (int k = 0; k < col1; k++) {
				product[i][j] += matrix1[i][k] * matrix2[k][j];
			}
		}
	}
	print(product, row1, col2);
}

void print(double **matrix, int row, int col)
{
	for (int i = 0; i < row; i++) {
		for (int j = 0; j < col; j++) {
			cout << "[" << matrix[i][j] << "]";
		}
		cout << "\n";
	}
}


