using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix
{
    public   class MatrixProcess
    {

          public mat hidd(mat mat)
        {
            mat m = new mat(mat.m + 1, 1);
            for (int i = 0; i < mat.m; i++)
            {
                m.matrex1[i, 0] = mat.matrex1[i, 0];
            }
            m.matrex1[mat.m, 0] = 1;
            return m;
        }


        //--------------------------------------------------------------------------------------------------------------

        public   mat choosing(mat desire, int ln)
        {
            desire = Calculate_Matrix(desire, 'z', 0);
            desire.matrex1[ln, 0] = 1;
            return desire;
        }


        //---------------------------------------------------------------------------------------------------------------

          public mat Calculate_Matrix(mat mat1, mat mat2, char calc)
        {
            switch (calc)
            {
                case '+':
                    return Adding(mat1, mat2);

                case '-':
                    return Subing(mat1, mat2);

                case '*':
                    return Producting(mat1, mat2);

                case 'o':
                    return Ones(mat1);

                case 'z':
                    return Zeros(mat1);
                case 'a':
                    return Multple_array(mat1, mat2);

                case 's':
                    return Multple_single(mat1, 0); // num);

                case 'r':
                    Console.WriteLine("how many rows  ");
                    int num_rows = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("which column ");
                    int which_column = Convert.ToInt32(Console.ReadLine());
                    return cutting_row(mat1, num_rows, which_column);

                case 'c':
                    Console.WriteLine("how many column  ");
                    int num_column = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("which row ");
                    int which_row = Convert.ToInt32(Console.ReadLine());
                    return cutting_column(mat1, num_column, which_row);

                case 't':
                    return Transpose(mat1);

                case 'd':
                    return randumming(mat1);

                default:
                    return mat1;
            }

        }

        //--------------------------------------------------------------------------------------
          public mat Calculate_Matrix(mat mat1, char calc, double rowOrcolumn)
        {

            switch (calc)
            {
                case 'o':
                    return Ones(mat1);

                case 'z':
                    return Zeros(mat1);

                case 's':
                    return Multple_single(mat1, rowOrcolumn);

                case 'r':
                    return cutting_row(mat1, 1, (mat1.n) - 1, (int)rowOrcolumn);

                case 'c':
                    return cutting_column(mat1, 1, mat1.m, (int)rowOrcolumn);

                case 't':
                    return Transpose(mat1);

                case 'd':
                    return randumming(mat1);

                default:
                    return mat1;
            }
        }

        //-------------------------------------------------------------------------------------------

          public mat cutting_column(mat mat1, int n, int m, int y)
        {
            mat temp;

            int[] array = new int[1];

            array[0] = y;

            temp = new mat(m + 1, n);

            int u = -1;

            for (int j = 0; j < temp.n; j++)
            {
                u++;
                n = array[u];
                for (int i = 0; i < temp.m; i++)
                {
                    temp.matrex1[i, j] = mat1.matrex1[i, n];
                }
            }

            return temp;
        }

        //------------------------------------------------------------------------------------------------------

          public mat cutting_row(mat mat1, int m, int n, int y)
        {
            mat temp;
            int[] array = new int[1];
            array[0] = y;

            temp = new mat(m, n + 1);

            int u = -1;

            for (int j = 0; j < temp.m; j++)
            {
                u++;
                m = array[u];
                for (int i = 0; i < temp.n; i++)
                {
                    temp.matrex1[j, i] = mat1.matrex1[m, i];

                }
            }

            return temp;
        }


          public mat Adding(mat mat1, mat mat2)
        {
            mat temp = new mat(mat1.m, mat1.n);

            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    temp.matrex1[i, j] = mat1.matrex1[i, j] + mat2.matrex1[i, j];
                }
            }
            return temp;
        }

          public mat Subing(mat mat1, mat mat2)
        {
            mat temp = new mat(mat1.m, mat1.n);

            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    temp.matrex1[i, j] = mat1.matrex1[i, j] - mat2.matrex1[i, j];
                }
            }
            return temp;
        }


          public mat Producting(mat mat1, mat mat2)
        {
            mat temp;

            int m1 = mat1.m, n1 = mat1.n, m2 = mat2.m, n2 = mat2.n;
            double temp_pro = 0;

            temp = new mat(m1, n2);
            for (int j = 0; j < m1; j++)
            {
                for (int u = 0; u < n2; u++)
                {

                    for (int i = 0; i < n1; i++)
                    {
                        temp_pro += mat1.matrex1[j, i] * mat2.matrex1[i, u];
                    }
                    temp.matrex1[j, u] = temp_pro;
                    temp_pro = 0;
                }
            }
            return temp;
        }


          public mat Multple_array(mat mat1, mat mat2)
        {
            mat temp = new mat(mat1.m, mat1.n);

            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    temp.matrex1[i, j] = Math.Round(mat1.matrex1[i, j] * mat2.matrex1[i, j], 6);
                }
            }
            return temp;
        }

          public mat Multple_single(mat mat1, double num)
        {
            mat temp = new mat(mat1.m, mat1.n);

            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    temp.matrex1[i, j] = Math.Round(mat1.matrex1[i, j] * num, 6);
                }
            }
            return temp;
        }

          public mat Zeros(mat mat1)
        {


            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    mat1.matrex1[i, j] = 0;
                }
            }
            return mat1;

        }

          public mat Ones(mat mat1)
        {
            mat temp = new mat(mat1.m, mat1.n);

            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    temp.matrex1[i, j] = 1;
                }
            }
            return temp;

        }

          public mat cutting_row(mat mat1, int m, int n)
        {
            mat temp;

            int[] array = new int[n];

            temp = new mat(m, n + 1);

            int u = -1;

            for (int j = 0; j < temp.m; j++)
            {
                u++;
                m = array[u];
                for (int i = 0; i < temp.n; i++)
                {
                    temp.matrex1[j, i] = mat1.matrex1[m, i];

                }
            }


            return temp;

        }


          public mat cutting_column(mat mat1, int n, int m)
        {

            mat temp;

            int[] array = new int[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter number of column ");
                array[i] = Convert.ToInt32(Console.ReadLine());
            }

            temp = new mat(m + 1, n);

            int u = -1;

            for (int j = 0; j < temp.n; j++)
            {
                u++;
                n = array[u];
                for (int i = 0; i < temp.m; i++)
                {


                    temp.matrex1[i, j] = mat1.matrex1[i, n];

                }

            }


            return temp;


        }


          public mat Transpose(mat mat1)
        {
            mat temp = new mat(mat1.n, mat1.m);

            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {

                    temp.matrex1[j, i] = mat1.matrex1[i, j];
                }
            }

            return temp;
        }

          public mat randumming(mat mat1)
        {

            double value = 0.1;
            Random randNum = new Random();

            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    mat1.matrex1[i, j] = Math.Round((value * 2 * randNum.NextDouble()) - value, 6);

                }
            }

            return mat1;

        }



          public void entering(mat mat)
        {
            for (int i = 0; i < mat.m; i++)
            {
                for (int j = 0; j < mat.n; j++)
                {
                    Console.WriteLine("Enter the value ");
                    mat.matrex1[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }

            for (int i = 0; i < mat.m; i++)
            {
                for (int j = 0; j < mat.n; j++)
                {
                    Console.Write(mat.matrex1[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
          public void outing(mat temp)
        {
            Console.WriteLine();
            int uu = 1;
            for (int i = 0; i < temp.m; i++)
            {
                for (int j = 0; j < temp.n; j++)
                {
                    Console.Write(Math.Round(temp.matrex1[i, j], 3) + "  ");
                    uu++;
                }
                Console.WriteLine();
            }
        }

    }
}
