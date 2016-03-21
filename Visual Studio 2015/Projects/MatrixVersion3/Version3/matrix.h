#pragma once
class matrix
{
private:
	int row, col;
	double **MatrixArray;
public:
	matrix(int, int); 
	matrix(const matrix& m); 
	~matrix(); 
	void fillMatrix();
	void print();
	matrix operator +(const matrix&);
	matrix operator -(const matrix&);
	matrix operator *(const matrix&);
	matrix operator =(const matrix&);
};

