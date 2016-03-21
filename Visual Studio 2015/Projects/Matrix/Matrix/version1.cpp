
#include <iostream>
using namespace std;

void addition(int row, int col);
void subtraction(int row, int col);
void multiplication();
void print(int row, int col);

const int MAX = 5;
double matrix1[MAX][MAX] = { 
{ 1.0, 2.0, 3.0, 4.0, 5.0 },
{ 2.0, 2.0, 2.0, 2.0, 2.0 },
{ 3.0, 1.0, 1.0, 1.0, 3.0 },
{ 0.0, 0.0, 2.0, 3.0, 2.0 },
{ 4.0, 4.0, -4.0, 0.0, 0.0 } };

double matrix2[MAX][MAX] = { 
{ 1.0, 0.0, 0.0, 0.0, 0.0 },
{ 1.0, 2.0, 1.0, 2.0, 1.0 },
{ 0.0, 0.0, 1.0, 0.0, 0.0 },
{ 1.0, 1.0, 1.0, 1.0, 1.0 },
{ 2.0, 2.0, -2.0, 2.0, 2.0 } };

int main() {
	print(MAX, MAX);
	addition(MAX, MAX);
	subtraction(MAX, MAX);
	multiplication();
	getchar();
}

void addition(int row, int col) {
	cout << "Sum:" << endl;
	double sum[MAX][MAX];
	for (int i = 0; i < row; i++) {
		for (int j = 0; j < col; j++) {
			sum[i][j] = matrix1[i][j] + matrix2[i][j];
			cout << "[" << sum[i][j] << " ]";
		}
		cout << "\n";
	}
	cout << "\n";
}

void subtraction(int row, int col) {
	cout << "Difference:" << endl;
	double difference[MAX][MAX];
	for (int i = 0; i < row; i++) {
		for (int j = 0; j < col; j++) {
			difference[i][j] = matrix1[i][j] - matrix2[i][j];
			cout << "[" << difference[i][j] << "]";
		}
		cout << "\n";
	}
	cout << "\n";
}

void multiplication() {
	cout << "Product:" << endl;
	double product[MAX][MAX] = {};
	for (int row = 0; row < 5; row++) {
		for (int col = 0; col < 5; col++) {
			for (int k = 0; k < 5; k++) {
				product[row][col] += matrix1[row][k] * matrix2[k][col];
			}
			cout << "[" <<  product[row][col] << "]";
		}
		cout << "\n";
	}
	cout << "\n";
}

void print(int row, int col) {

	cout << "Matrix 1:\n";
	for (int i = 0; i < row; i++) {
		for (int j = 0; j < col; j++) {
			cout << "[" << matrix1[i][j] << "]";
		}
		cout << "\n";
	}
	cout << "\n";
	cout << "Matrix 2:\n";
	for (int i = 0; i < row; i++) {
		for (int j = 0; j < col; j++) {
			cout << "[" << matrix2[i][j] << "]";
		}
		cout << "\n";
	}
	cout << "\n";
}

/*----------------OUTPUT----------------
Matrix 1:
[1][2][3][4][5]
[2][2][2][2][2]
[3][1][1][1][3]
[0][0][2][3][2]
[4][4][-4][0][0]

Matrix 2:
[1][0][0][0][0]
[1][2][1][2][1]
[0][0][1][0][0]
[1][1][1][1][1]
[2][2][-2][2][2]

Sum:
[2 ][2 ][3 ][4 ][5 ]
[3 ][4 ][3 ][4 ][3 ]
[3 ][1 ][2 ][1 ][3 ]
[1 ][1 ][3 ][4 ][3 ]
[6 ][6 ][-6 ][2 ][2 ]

Difference:
[0][2][3][4][5]
[1][0][1][0][1]
[3][1][0][1][3]
[-1][-1][1][2][1]
[2][2][-2][-2][-2]

Product:
[17][18][-1][18][16]
[10][10][2][10][8]
[11][9][-3][9][8]
[7][7][1][7][7]
[8][8][0][8][4]

*/