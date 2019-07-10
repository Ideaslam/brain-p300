using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix
{
   public  class NeuralNetwork
    {
        public double lamda = 0.7;
        public int alldata;
        public int numColumn = 100;
        public mat pic;
        public mat input;
        public mat hidden;
        public mat output;
        public mat hidden_weight;
        public mat hidden_deltaweight;
        public mat output_deltaweight;
        public mat hidd_temp;
        public mat output_weight;
        public mat error;
        public mat desire;
        public int ln;

        public MatrixProcess matrixProcess = new MatrixProcess();




        public int input_num, hidden_num, output_num;


        public double num = 0, num2 = 0;

        public double toterror = 0;

        public void forword()
        {
            hidd_temp = matrixProcess.Calculate_Matrix(hidden_weight, input, '*');
            hidd_temp = out_sig(hidd_temp);
            hidden = matrixProcess.hidd(hidd_temp);

            output = matrixProcess.Calculate_Matrix(output_weight, hidden, '*');
            output = out_sig(output);

            desire = matrixProcess.choosing(desire, ln);
            error = erroring(desire, output);
            toterror += total_error(error);
        }
        //-------------------------------------------------------------------------------------
        public int j = 0;
        public void backword()
        {
            // double temp2=0; 
            mat ones = new mat(output_num, 1);
            mat backtemp = new mat(hidden_num, 1);
            mat output_error = new mat(output_num, 1);
            mat backtemp2 = new mat(hidden_num - 1, 1);

            ones = matrixProcess.Calculate_Matrix(ones, 'o', 0);
            output_error = matrixProcess.Calculate_Matrix(error, matrixProcess.Calculate_Matrix(output, matrixProcess.Calculate_Matrix(ones, output, '-'), 'a'), 'a');
            output_deltaweight = matrixProcess.Calculate_Matrix(output_deltaweight, matrixProcess.Calculate_Matrix(output_error, matrixProcess.Calculate_Matrix(hidden, 't', 0), '*'), '+');


            backtemp = matrixProcess.Calculate_Matrix(matrixProcess.Calculate_Matrix(output_weight, 't', 0), output_error, '*');
            for (int i = 0; i < hidden_num - 1; i++)
            {
                backtemp2.matrex1[i, 0] = backtemp.matrex1[i, 0] * hidden.matrex1[i, 0] * (1 - hidden.matrex1[i, 0]);
            }
            hidden_deltaweight = matrixProcess.Calculate_Matrix(hidden_deltaweight, matrixProcess.Calculate_Matrix(backtemp2, matrixProcess.Calculate_Matrix(input, 't', 0), '*'), '+');
            j++;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------


        public void update_weight(double number)
        {
            num = 1 / number;
            output_weight = matrixProcess.Calculate_Matrix(output_weight, matrixProcess.Calculate_Matrix(output_deltaweight, 's', num), '-');
            hidden_weight = matrixProcess.Calculate_Matrix(hidden_weight, matrixProcess.Calculate_Matrix(hidden_deltaweight, 's', num), '-');
        }

        //---------------------------------------------------------------------------------------------------------------------
        public mat erroring(mat d, mat o)
        {
            mat e = new mat(output.m, output.n);
            e = matrixProcess.Calculate_Matrix(o, d, '-');
            return e;
        }
        //---------------------------------------------------------------------------------------------------------------------

        public double total_error(mat e)
        {
            double tot = 0;
            for (int j = 0; j < e.m; j++)
            {
                tot += Math.Pow(e.matrex1[j, 0], 2);
                //tot += (-1*desire.matrex1[j,0]*Math.Log(output.matrex1[j,0]) - (1-desire.matrex1[j,0])*Math.Log(1-output.matrex1[j,0]));
            }

            return tot / 2;
        }

        //------------------------------------------------------------------------------------------------


        public void order(mat pic)
        {

            input = rescaling(pic);

            forword();
            backword();
        }

        public void initiation()
        {
            hidden_weight = matrixProcess.Calculate_Matrix(hidden_weight, 'd', 0);
            output_weight = matrixProcess.Calculate_Matrix(output_weight, 'd', 0);

        }

        //---------------------------------------------------------------------------------------------------
        public mat rescaling(mat input)
        {

            double max = input.matrex1[0, 0], min = input.matrex1[0, 0]; // max 


            for (int i = 0; i < input.m; i++)
            {
                if (input.matrex1[i, 0] > max)
                {
                    max = input.matrex1[i, 0];
                }

            }

            for (int i = 0; i < input.m - 1; i++) // min 
            {
                if (input.matrex1[i, 0] < min)
                {
                    min = input.matrex1[i, 0];
                }

            }


            for (int i = 0; i < input_num; i++) // normalization [0,1] 
            {
                double sub = (input.matrex1[i, 0]) - min;

                input.matrex1[i, 0] = (sub / (max - min));
            }
            input.matrex1[input_num - 1, 0] = 1;
            return input;
        }

        //--------------------------------------------------------------------------------------------------
        public mat out_sig(mat mat1)
        {
            mat temping = new mat(mat1.m, mat1.n);


            for (int i = 0; i < mat1.m; i++)
            {
                for (int j = 0; j < mat1.n; j++)
                {
                    temping.matrex1[i, j] = 1 / (1 + Math.Exp(-1 * lamda * mat1.matrex1[i, j]));
                }
            }

            return temping;
        }
        //------------------------------------------------------------------------------------------------
    
    }
}
