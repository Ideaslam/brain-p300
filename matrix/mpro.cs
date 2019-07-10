using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging ;
using System.IO;


namespace matrix
{

   public class mpro
    {
       

// ---------------------------------------------------------------------------------------
     static void Main(string[] args)
        {
            NeuralNetwork NN = new NeuralNetwork();
            Console.WriteLine("Enter input number ");
            NN.input_num = Convert.ToInt32(Console.ReadLine());
            NN.input_num++;

           Console.WriteLine("Enter hidden number ");
            NN.hidden_num = Convert.ToInt32(Console.ReadLine());
            NN.hidden_num++;

           Console.WriteLine("Enter output number ");
            NN.output_num = Convert.ToInt32(Console.ReadLine());


            NN.pic = new mat(NN.input_num, 1);
            NN.input = new mat(NN.input_num, 1);
            NN.hidden = new mat(NN.hidden_num, 1);
            NN.output = new mat(NN.output_num, 1);
            NN.hidden_weight = new mat(NN.hidden_num -1, NN.input_num);
            NN.output_weight = new mat(NN.output_num, NN.hidden_num);
            NN.hidd_temp = new mat(NN.hidden_num -1, 1);

            NN.hidden_deltaweight = new mat(NN.hidden_num -1, NN.input_num);
            NN.output_deltaweight = new mat(NN.output_num, NN.hidden_num);

            NN.desire = new mat(NN.output_num, 1);
            NN.error = new mat(NN.output_num, 1);
            NN.pic = new mat(NN.input_num, 1);


            NN.initiation();

            int num = 1;

            for (int u = 0; u < 1000; u++)//iterations 
            {





                imaging(NN);
                NN.update_weight(NN.alldata);

                NN.hidden_deltaweight = NN.matrixProcess.Calculate_Matrix(NN.hidden_deltaweight, 'z', 0);
                NN.output_deltaweight = NN.matrixProcess.Calculate_Matrix(NN.output_deltaweight, 'z', 0);
                Console.WriteLine(u+"--"+(NN.toterror = NN.toterror / NN.alldata));


                NN.toterror = 0;

            }



            while (true)
            {

                Console.WriteLine("Enter the Signal  :");
                string name = Console.ReadLine();
                var filePath = @name;
                var data = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();
                int actualMax=0;
                

                double max = 0 ,fpr ,tpr,fnr ,tnr ,accuracy;
                int tryMax = 0;
                int fp = 0, fn = 0, tp = 0, tn = 0;
                



                for (int i =0; i< data.Length; i++)
                {
                    

                    for (int j=0; j< NN.input_num -1; j++)
                    {
                        NN.pic.matrex1[j, 0] = Convert.ToDouble(data[i][j].ToString());
                     
                    }
                    NN.pic.matrex1[NN.input_num - 1, 0] = 1;
                    actualMax = Convert.ToInt32(data[i][NN.input_num -1].ToString());
                    NN.input = NN.rescaling(NN.pic);
                    NN.forword();





                    for (int k = 0; k < NN.output_num; k++)
                    {
                        Console.WriteLine(k + ": " + NN.output.matrex1[k, 0]);
                    }


                    for (int k = 0; k < NN.output_num; k++)
                    {
                        if (NN.output.matrex1[k, 0] > max)
                        {
                            max = NN.output.matrex1[k, 0];
                            tryMax = k;
                        }

                    }
                    
                    

                    if (tryMax == 1 && actualMax == 1)
                    {
                        tn++;

                    }
                    else if (tryMax == 0 && actualMax == 1)
                    {
                        fp++;

                    }
                    else if (tryMax == 1 && actualMax == 0)
                    {
                        fn++;
                    }
                    else if (tryMax == 0 && actualMax == 0)
                    {
                        tp++;
                    }
                    
                    Console.WriteLine("-- "+i+" --" + max + " - max - " + tryMax +" total error "+ NN.toterror);
                    max = 0;
                    NN.toterror = 0;
                    tryMax = 0;


                }

                Console.WriteLine("FP :" + fp);
                Console.WriteLine("FN :" + fn);
                Console.WriteLine("TP :" + tp);
                Console.WriteLine("TN :" + tn);

                fpr = (double)fp/(fp + tn);//fall out 
                tpr = (double)tp /(tp + fn);//recall
                fnr = (double)fn /(tp + fn);//miss rate
                tnr = (double)tn /(fp + tn);//specify
                accuracy =(double) (tp + tn) / (tp + tn + fn + fp);
                Console.WriteLine();
                Console.WriteLine("FPR(FallOut)  :" + fpr );
                Console.WriteLine("TPR(ReCall)   :" +tpr);
                Console.WriteLine("FNR(MissRate) :" +fnr );
                Console.WriteLine("TNR(Specify)  :" +tnr);
                Console.WriteLine("ACCURACY      :" +accuracy );



            }
            Console.ReadLine();
        }
        //----------------------------------------------------------------------------------------



       static public void imaging(NeuralNetwork NN) // prepare dataSet
        {

            NN.ln = 0;
            var filePath = @"C:\Users\islam\Desktop\Epoc\pAva.csv";
            var data = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();
            int trainingLenght = data.Length;
            NN.alldata = trainingLenght * 2;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < NN.numColumn; j++)
                {
                    NN.pic.matrex1[j, 0] = Convert.ToDouble(data[i][j].ToString());

                }
                NN.pic.matrex1[NN.input_num - 1, 0] = 1;
                NN.order(NN.pic);
            }



            NN.ln = 1;
            filePath = @"C:\Users\islam\Desktop\Epoc\nonAva.csv";
            data = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();

            for (int i = 0; i < trainingLenght; i++)
            {
                for (int j = 0; j < NN.numColumn; j++)
                {
                    NN.pic.matrex1[j, 0] = Convert.ToDouble(data[i][j].ToString());

                }
                NN.pic.matrex1[NN.input_num - 1, 0] = 1;
                NN.order(NN.pic);
            }







        }



    }
}
