using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFT_BLE_expert_
{
    class Filter
    {
        int input_len, output_len ,order1,order2, i, j,k,o,naxpy,removetemp;
        float temp1;
        double[] temp, x,y, lowfc,highfc, dv0, dv1, Sportlowfc, Sporthighfc;
        float[] outsignal;

        public Filter(int inputlength,int outputlength, int order, int order_2)
        {

            this.input_len = inputlength;
            this.output_len = outputlength;
            this.order1 = order;
            this.order2 = order_2;
            //this.lowfc = new double[] {0.00321258,0.001451138,-0.009152126,-0.027502172,-0.019587756,0.057330886,0.192192036,0.30205541,0.30205541,0.192192036,
              //    0.057330886,-0.019587756,-0.027502172,-0.009152126,0.001451138,0.003212583};
            this.Sportlowfc = new double[] {0.003213 ,0.001451 ,-0.009152 ,-0.027502 ,-0.019588 ,0.057331 ,0.192192 ,0.302055 ,0.302055 ,0.192192 ,0.057331 ,
                -0.019588 ,-0.027502 ,-0.009152 ,0.001451 ,0.003213};
            
            this.lowfc = new double[] {-0.001618,-0.001857,-0.001082,0.001643,0.006095,0.009407,0.006795,-0.004879,-0.022634,-0.035634,-0.028883,0.008476,0.074753,
                      0.152895,0.216232,0.240584,0.216232,0.152895,0.074753,0.008476,-0.028883,-0.035634,-0.022634,-0.004879,0.006795,0.009407,0.006095,
                      0.001643,-0.001082,-0.001857,-0.001618};
            
            this.Sporthighfc = new double[]{0.000000 ,0.000031 ,0.000065 ,0.000102 ,0.000144 ,0.000192 ,0.000248 ,0.000312 ,0.000387 ,0.000472 ,0.000568 ,0.000677 ,
                0.000799 ,0.000933 ,0.001081 ,0.001241 ,0.001413 ,0.001596 ,0.001788 ,0.001988 ,0.002194 ,0.002403 ,0.002613 ,0.002820 ,0.003021 ,0.003212 ,0.003388 ,
                0.003546 ,0.003680 ,0.003786 ,0.003857 ,0.003888 ,0.003874 ,0.003807 ,0.003682 ,0.003492 ,0.003229 ,0.002887 ,0.002458 ,0.001933 ,0.001305 ,0.000563 ,
                -0.000303 ,-0.001304 ,-0.002454 ,-0.003769 ,-0.005267 ,-0.006971 ,-0.008910 ,-0.011121 ,-0.013653 ,-0.016571 ,-0.019967 ,-0.023971 ,-0.028776 ,-0.034676 ,
                -0.042154 ,-0.052043 ,-0.065920 ,-0.087173 ,-0.124613 ,-0.210572 ,-0.636065 ,0.636065 ,0.210572 ,0.124613 ,0.087173 ,0.065920 ,0.052043 ,0.042154 ,0.034676 ,
                0.028776 ,0.023971 ,0.019967 ,0.016571 ,0.013653 ,0.011121 ,0.008910 ,0.006971 ,0.005267 ,0.003769 ,0.002454 ,0.001304 ,0.000303 ,-0.000563 ,-0.001305 ,-0.001933 ,
                -0.002458 ,-0.002887 ,-0.003229 ,-0.003492 ,-0.003682 ,-0.003807 ,-0.003874 ,-0.003888 ,-0.003857 ,-0.003786 ,-0.003680 ,-0.003546 ,-0.003388 ,-0.003212 ,-0.003021 ,
                -0.002820 ,-0.002613 ,-0.002403 ,-0.002194 ,-0.001988 ,-0.001788 ,-0.001596 ,-0.001413 ,-0.001241 ,-0.001081 ,-0.000933 ,-0.000799 ,-0.000677 ,-0.000568 ,-0.000472 ,
                -0.000387 ,-0.000312 ,-0.000248 ,-0.000192 ,-0.000144 ,-0.000102 ,-0.000065 ,-0.000031 ,0.000000};
            
            this.highfc = new double[]{-0.000288,-0.000299,-0.000314,-0.000334,-0.000360,-0.000392,-0.000430,-0.000475,-0.000528,-0.000588,-0.000656,-0.000732,-0.000818,
                                          -0.000913,-0.001018,-0.001134,-0.001261,-0.001400,-0.001551,-0.001714,-0.001892,-0.002083,-0.002290,-0.002512,-0.002751,-0.003008,-0.003283,-0.003578,
                                      -0.003894,-0.004232,-0.004593,-0.004979,-0.005393,-0.005836,-0.006310,-0.006818,-0.007363,-0.007949,-0.008579,-0.009259,-0.009994,-0.010791,-0.011658,
                                      -0.012604,-0.013642,-0.014786,-0.016054,-0.017469,-0.019061,-0.020866,-0.022935,-0.025336,-0.028163,-0.031547,-0.035685,-0.040876,-0.047606,-0.056712,
                                      -0.069775,-0.090187,-0.126764,-0.211831,-0.636334,0.636334,0.211831,0.126764,0.090187,0.069775,0.056712,0.047606,0.040876,0.035685,0.031547,0.028163,
                                      0.025336,0.022935,0.020866,0.019061,0.017469,0.016054,0.014786,0.013642,0.012604,0.011658,0.010791,0.009994,0.009259,0.008579,0.007949,0.007363,0.006818,
                                      0.006310,0.005836,0.005393,0.004979,0.004593,0.004232,0.003894,0.003578,0.003283,0.003008,0.002751,0.002512,0.002290,0.002083,0.001892,0.001714,0.001551,
                                      0.001400,0.001261,0.001134,0.001018,0.000913,0.000818,0.000732,0.000656,0.000588,0.000528,0.000475,0.000430,0.000392,0.000360,0.000334,0.000314,0.000299,0.000288};
                                      
            dv0 = new double[] { 0.991153595101663, -1.98230719020333, 0.991153595101663 };
            dv0 = new double[] { 0.991153595101663, -1.98230719020333, 0.991153595101663 };
            dv1 = new double[] { 1.0, -1.98222892979253, 0.982385450614125 };
            
            removetemp = input_len - output_len;
        }
        public float [] Lowpassfilter(float[] Signal1)
        {
            double[] x = new double[input_len];
            double[] y = new double[input_len];
            double[] temp = new double[input_len];
            outsignal = new float[output_len];

            for (i = 0; i < input_len; i++)
            {
                if (i < order1)
                {
                    for (j = 0; j <= order1; j++)
                    {
                        if (j < order1 - i)
                            x[j] = 0;
                        else
                            x[j] = Signal1[j - order1 + i];
                    }
                }
                else
                {
                    for (j = 0; j <= order1; j++)
                    {
                        x[j] = Signal1[i - order1 + j];
                    }
                }
                for (j = 0; j <= order1; j++)
                {
                    y[i] = y[i] + x[j] * lowfc[j];
                }
            }
            for (i = 0; i < output_len; i++)
            {
                outsignal[i] = (float)y[i + removetemp];
            }
            return outsignal;

        }
        public float[] SportLowpassfilter(float[] Signal1)
        {
            double[] x = new double[input_len];
            double[] y = new double[input_len];
            double[] temp = new double[input_len];
            outsignal = new float[output_len];

            for (i = 0; i < input_len; i++)
            {
                if (i < order1)
                {
                    for (j = 0; j <= order1; j++)
                    {
                        if (j < order1 - i)
                            x[j] = 0;
                        else
                            x[j] = Signal1[j - order1 + i];
                    }
                }
                else
                {
                    for (j = 0; j <= order1; j++)
                    {
                        x[j] = Signal1[i - order1 + j];
                    }
                }
                for (j = 0; j <= order1; j++)
                {
                    y[i] = y[i] + x[j] * Sportlowfc[j];
                }
            }
            for (i = 0; i < output_len; i++)
            {
                outsignal[i] = (float)y[i + removetemp];
            }
            return outsignal;

        }
        public float[] bandpassfilter(float[] Signal1)
        {

            double[] x = new double[input_len];
            double[] y = new double[input_len];
            double[] temp = new double[input_len];
            outsignal = new float[output_len];

            //lowpass
            for (i = 0; i < input_len; i++){
                if (i < order1)
                {
                    for (j = 0; j <= order1; j++){
                        if (j < order1 - i)
                            x[j] = 0;
                        else
                            x[j] = Signal1[j - order1 + i];
                    }
                }
                else
                {
                    for (j = 0; j <= order1; j++){
                        x[j] = Signal1[i - order1 + j];
                    }
                }
                for (j = 0; j <= order1; j++) {
                    y[i] = y[i] + x[j] * lowfc[j];
                }
            }
            for (i = 0; i < input_len; i++){
                temp[i] = y[i];
                y[i] = 0;
            }

            //highpass
            for (i = 0; i < input_len; i++){
                if (i < order2)
                {
                    for (j = 0; j <= order2; j++){
                        if (j < order2 - i)
                            x[j] = 0;
                        else
                            x[j] = temp[j - order2 + i];
                    }
                }
                else
                {
                    for (j = 0; j <= order2; j++){
                        x[j] = temp[i - order2 + j];
                    }
                }
                for (j = 0; j <= order2; j++){
                    y[i] = y[i] + x[j] * highfc[j];
                }
            }
            for (i = 0; i < output_len; i++){
                outsignal[i] = (float)y[i + removetemp] * (-1);
            }
            return outsignal;
        }

        public float[] Sportbandpassfilter(float[] Signal1)
        {

            double[] x = new double[input_len];
            double[] y = new double[input_len];
            double[] temp = new double[input_len];
            outsignal = new float[output_len];

            //lowpass
            for (i = 0; i < input_len; i++)
            {
                if (i < order1)
                {
                    for (j = 0; j <= order1; j++)
                    {
                        if (j < order1 - i)
                            x[j] = 0;
                        else
                            x[j] = Signal1[j - order1 + i];
                    }
                }
                else
                {
                    for (j = 0; j <= order1; j++)
                    {
                        x[j] = Signal1[i - order1 + j];
                    }
                }
                for (j = 0; j <= order1; j++)
                {
                    y[i] = y[i] + x[j] * Sportlowfc[j];
                }
            }
            for (i = 0; i < input_len; i++)
            {
                temp[i] = y[i];
                y[i] = 0;
            }

            //highpass
            for (i = 0; i < input_len; i++)
            {
                if (i < order2)
                {
                    for (j = 0; j <= order2; j++)
                    {
                        if (j < order2 - i)
                            x[j] = 0;
                        else
                            x[j] = temp[j - order2 + i];
                    }
                }
                else
                {
                    for (j = 0; j <= order2; j++)
                    {
                        x[j] = temp[i - order2 + j];
                    }
                }
                for (j = 0; j <= order2; j++)
                {
                    y[i] = y[i] + x[j] * Sporthighfc[j];
                }
            }
            for (i = 0; i < output_len; i++)
            {
                outsignal[i] = (float)y[i + removetemp] * (-1);
            }
            return outsignal;
        }

        public float[] newbandpassfilter(float[] Signal1)
        {
            double[] x = new double[output_len];
            double[] y = new double[output_len];
            double[] temp = new double[output_len];
            //lowpass
            for (i = 0; i < input_len; i++)
            {
                if (i < order1)
                {
                    for (j = 0; j <= order1; j++)
                    {
                        if (j < order1 - i)
                            x[j] = 0;
                        else
                            x[j] = Signal1[j - order1 + i];
                    }
                }
                else
                {
                    for (j = 0; j <= order1; j++)
                    {
                        x[j] = Signal1[i - order1 + j];
                    }
                }
                for (j = 0; j <= order1; j++)
                {
                    y[i] = y[i] + x[j] * lowfc[j];
                }
            }
            for (i = 0; i < input_len; i++)
            {
                temp[i] = y[i];
                y[i] = 0;
            }

            //highpass
            for (k = 0; k < 1000; k++)
            {
                naxpy = 1000 - k;
                if (!(naxpy < 3))
                {
                    naxpy = 3;
                }

                for (o = 0; o < naxpy; o++)
                {
                    y[k +o] += (float)temp[k] * (float)dv0[o];
                }
                naxpy = 999 - k;
                if (!(naxpy < 2))
                {
                    naxpy = 2;
                }

                temp1 = -(float)y[k];
                for (o = 1; j <= naxpy; o++)
                {
                    y[k + o] += temp1 * (float)dv1[o];
                }
            }
            for (i = 0; i < output_len; i++)
            {
                outsignal[i] = (float)y[i + removetemp];
            }
            return outsignal;
        }

    }   
}
