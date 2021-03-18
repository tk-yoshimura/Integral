using System;
using System.Collections.Generic;

namespace Integral {

	/// <summary>Richardsonの補外法</summary>
	public class RichardsonExtrapolation {
        readonly List<double[]> values = new List<double[]>();

        /// <summary>収束数列値を挿入</summary>
        public void Inject(double new_value) {
            if(SeriesCount <= 0) {
                values.Add(new double[] { new_value });
                return;
            }

            double[] t = values[SeriesCount - 1], t_next = new double[SeriesCount + 1];

            t_next[0] = new_value;

            double r = 1;
            for(int i = 1; i <= SeriesCount; i++) {
                r *= 4;
                t_next[i] = t_next[i - 1] + (t_next[i - 1] - t[i - 1]) / (r - 1);
            }

            values.Add(t_next);
        }

        /// <summary>補外数列</summary>
        public IEnumerable<double> Series {
            get {
                for(int i = 0; i < values.Count; i++) {
                    yield return values[i][i];
                }
            }
        }

        /// <summary>収束推定値</summary>
        public double ConvergenceValue {
            get {
                if(SeriesCount <= 0) {
                    throw new InvalidOperationException();
                }

                return values[SeriesCount - 1][SeriesCount - 1];
            }
        }

        /// <summary>補外数列の要素数</summary>
        public int SeriesCount => values.Count;

        /// <summary>推定最大誤差</summary>
        public double Epsilon {
            get {
                if(SeriesCount <= 1) {
                    throw new InvalidOperationException();
                }

                return Math.Abs(values[SeriesCount - 1][SeriesCount - 1] - values[SeriesCount - 2][SeriesCount - 2]);
            }
        }
    }
}
