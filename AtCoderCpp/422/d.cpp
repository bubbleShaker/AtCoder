/**
 * AtCoder ABC422 D - Least Unbalanced
 *
 * ### 問題概要
 * 長さ 2^N、総和 K の非負整数列 A の「アンバランスさ」を、
 * 「Aの要素を隣接2項の和で置き換えていく操作をN回繰り返す各ステップでの、Aの(最大値 - 最小値) の最大値」
 * として定義する。アンバランスさが最小となるような初期数列 A と、そのときのアンバランスさ U を求める問題。
 *
 * ### 解法の考え方
 * アンバランスさを最小化するには、各ステップにおいて数列の要素ができるだけ均等になっていることが望ましい。
 * つまり、各ステップでの (最大値 - 最小値) を最小化することを目指す。
 *
 * 逆から考えると、Nステップ後の数列は要素が K のみの一つ。その前のステップ(N-1)では、
 * 合計がKになる2つの要素で、差が最小になるようにするには floor(K/2) と ceil(K/2) に分けるのが最適。
 * 同様に、その前のステップでは、それぞれの値をさらに均等に2分割していくのが最適だとわかる。
 *
 * このように、トップダウンで値を再帰的に分割していくことで、各ステップでの要素の差を常に 0 または 1 に
 * 抑えることができる。この戦略により、全体のアンバランスさも 0 または 1 になる。
 *
 * ### 具体的なアルゴリズム
 * 1. 最小アンバランスさ U の決定:
 *    - 総和 K が初期の要素数 2^N で割り切れる場合、全要素を K / 2^N にすれば、
 *      全てのステップで要素が均一になり、差は0。よって U = 0。
 *    - 割り切れない場合、初期数列で最低でも floor(K/2^N) と ceil(K/2^N) という1の差が生じる。
 *      上記の再帰的分割戦略により、アンバランスさを1に抑えることができる。よって U = 1。
 *
 * 2. 数列 A の構築:
 *    - `build(level, value)` という再帰関数を考える。
 *    - これは、「合計が `value` になる `2^level` 個の要素を持つ均等な数列」を構築する関数。
 *    - `level == 0` なら、それは要素 `value` そのもの。
 *    - `level > 0` なら、`value` を `v1 = value / 2` と `v2 = value - v1` に分割し、
 *      `build(level - 1, v1)` と `build(level - 1, v2)` を順に呼び出す。
 *    - `build(N, K)` を実行することで、目的の数列 A が得られる。
 */

#include <iostream>
#include <vector>
#include <cmath>
#include <numeric>

long long N, K;
std::vector<long long> result_array;

// level: あと何回分割するか (再帰の深さ)
// value: この部分木が担当する合計値
void build(int level, long long value) {
    // ベースケース: もう分割しない (level=0) なら、値を結果に追加
    if (level == 0) {
        result_array.push_back(value);
        return;
    }

    // 値をできるだけ均等に2分割する
    long long v1 = value / 2;
    long long v2 = value - v1; // ceil(value/2) と同じ

    // 分割した値で、さらに下の階層を構築する
    build(level - 1, v1);
    build(level - 1, v2);
}

int main() {
    // 高速化
    std::ios_base::sync_with_stdio(false);
    std::cin.tie(NULL);

    std::cin >> N >> K;

    // 2^N を計算 (N<=20 なので long long で十分)
    long long num_elements = 1LL << N;

    // 最小アンバランスさ U を計算して出力
    long long U = (K % num_elements == 0) ? 0 : 1;
    std::cout << U << "\n";

    // 再帰的に数列を構築
    build(N, K);

    // 構築した数列を出力
    for (int i = 0; i < result_array.size(); ++i) {
        std::cout << result_array[i] << (i == result_array.size() - 1 ? "" : " ");
    }
    std::cout << "\n";

    return 0;
}