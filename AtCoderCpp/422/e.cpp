/**
 * AtCoder ABC422 E - Majority Line
 *
 * ### 問題概要
 * N個の点が与えられる (Nは奇数)。これらの点のうち過半数 ( (N+1)/2 個以上) を通る直線が存在するか判定し、
 * 存在するならその直線の方程式 ax+by+c=0 の係数 a,b,c を出力する。
 *
 * ### 解法: 乱択アルゴリズム (Randomized Algorithm)
 * Nが最大 5*10^5 と非常に大きいため、全点のペアを試して直線を検証する O(N^3) や O(N^2 log N) の解法では間に合わない。
 * ここで、問題の性質を利用した乱択アルゴリズムが有効となる。
 *
 * #### アルゴリズムの核心
 * もし求める直線（以下、多数派直線）が存在するならば、その直線上には少なくとも K = (N+1)/2 個の点が存在する。
 * このとき、N個の点からランダムに2つの異なる点 p_i, p_j を選んだ場合、その2点ともが多数派直線の上に乗っている確率は
 * (K/N) * ((K-1)/(N-1)) ≒ (1/2) * (1/2) = 1/4 となり、かなり高い。
 *
 * したがって、以下の試行を十分な回数（例えば100回）繰り返せば、非常に高い確率で多数派直線を特定できる。
 * 1. ランダムに異なる2点 p_i, p_j を選ぶ。
 * 2. p_i, p_j を通る直線を求める。
 * 3. その直線が、N個の点のうち何個の点を通るか数える。
 * 4. もし通る点の数が K 個以上であれば、それが求める直線である。
 *
 * #### 実装の詳細
 * - **直線の係数:** 2点 (x1, y1), (x2, y2) を通る直線 ax+by+c=0 の係数は以下のように求められる。
 *   a = y2 - y1
 *   b = x1 - x2
 *   c = -a*x1 - b*y1 = x2*y1 - x1*y2
 *
 * - **共線判定:** 点(x3, y3)がこの直線上にあるかは、`a*x3 + b*y3 + c == 0` で判定する。
 *   x, yの座標が最大10^8なので、a*x3 などの中間生成物は 10^8 * 2*10^8 = 2*10^16 となり、long long の範囲に収まる。
 *   しかし、cも同様に大きくなる可能性があり、`a*x3 + b*y3 + c` の計算は long long の範囲を超える可能性があるため、
 *   128ビット整数型 `__int128_t` を用いてオーバーフローを防ぐ。
 *
 * - **試行回数:** 失敗確率を p = 1 - 1/4 = 3/4 とすると、T回の試行で失敗し続ける確率は (3/4)^T となる。
 *   T=100 とすれば、失敗確率は天文学的に小さくなり、実用上十分である。
 */

#include <iostream>
#include <vector>
#include <random>
#include <chrono>

struct Point {
    long long x, y;
};

int main() {
    std::ios_base::sync_with_stdio(false);
    std::cin.tie(NULL);

    int N;
    std::cin >> N;

    std::vector<Point> points(N);
    for (int i = 0; i < N; ++i) {
        std::cin >> points[i].x >> points[i].y;
    }

    if (N <= 2) {
        std::cout << "Yes\n";
        if (N == 1) {
             std::cout << "1 1 " << -points[0].x - points[0].y << "\n";
        } else {
            long long a = points[1].y - points[0].y;
            long long b = points[0].x - points[1].x;
            long long c = -a * points[0].x - b * points[0].y;
            std::cout << a << " " << b << " " << c << "\n";
        }
        return 0;
    }

    // 乱数生成器の準備
    std::mt19937 rng(std::chrono::steady_clock::now().time_since_epoch().count());
    std::uniform_int_distribution<int> dist(0, N - 1);

    int majority_count = (N + 1) / 2;
    int iterations = 100;

    for (int i = 0; i < iterations; ++i) {
        int idx1 = dist(rng);
        int idx2 = dist(rng);
        if (idx1 == idx2) {
            // 同じ点を選んでしまったらやり直し
            i--;
            continue;
        }

        Point p1 = points[idx1];
        Point p2 = points[idx2];

        // 直線の係数を計算
        long long a = p2.y - p1.y;
        long long b = p1.x - p2.x;
        // __int128_t を使ってcを計算し、オーバーフローを避ける
        __int128_t c_128 = (__int128_t)p2.x * p1.y - (__int128_t)p1.x * p2.y;

        int count = 0;
        for (int k = 0; k < N; ++k) {
            // 共線判定。オーバーフローを避けるため __int128_t で計算
            if ((__int128_t)a * points[k].x + (__int128_t)b * points[k].y + c_128 == 0) {
                count++;
            }
        }

        if (count >= majority_count) {
            std::cout << "Yes\n";
            long long c = (long long)c_128;
            std::cout << a << " " << b << " " << c << "\n";
            return 0;
        }
    }

    std::cout << "No\n";

    return 0;
}
