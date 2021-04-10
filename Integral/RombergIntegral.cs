using System;

namespace Integral {

	/// <summary>Romberg積分</summary>
	public static class RombergIntegral {

        /// <summary>積分</summary>
        /// <param name="func">対象の関数</param>
        /// <param name="a">下端</param>
        /// <param name="b">上端</param>
        /// <param name="precision_level">精度レベル</param>
        /// <remarks>有効桁数14-15桁 精度レベルは大きすぎるとかえって誤差が大きくなる</remarks>
        public static double Integrate(Func<double, double> func, double a, double b, int precision_level = 10) {
            if(func is null) {
                throw new ArgumentNullException(nameof(func));
            }
            if(!(a <= b)) {
                throw new ArgumentException($"{nameof(a)},{nameof(b)}");
            }
            if(precision_level < 1 || precision_level > 16) {
                throw new ArgumentOutOfRangeException(nameof(precision_level));
            }

            int max_div = 1 << precision_level;
            double h = b - a, min_h = (b - a) / max_div;
            double[] v = new double[max_div + 1];
            RichardsonExtrapolation conv = new();

            for(int i = 0; i <= max_div; i++) {
                v[i] = func(a + i * min_h);
            }

            double t = h * (v[0] + v[max_div]) / 2, new_t;
            conv.Inject(t);

            for(int s = max_div; s > 1; s /= 2) {
                new_t = 0;
                for(int i = s / 2; i < max_div; i += s) {
                    new_t += v[i];
                }

                h /= 2;
                t = t / 2 + h * new_t;

                conv.Inject(t);
            }

            return conv.ConvergenceValue;
        }
    }
}
