#include <iostream>
#include <vector>
#include <queue>
#include <atcoder/convolution>
#include <atcoder/dsu>
#include <atcoder/fenwicktree>
#include <atcoder/lazysegtree>
#include <atcoder/math>
#include <atcoder/maxflow>
#include <atcoder/mincostflow>
#include <atcoder/modint>
#include <atcoder/scc>
#include <atcoder/segtree>
#include <atcoder/segtree.hpp>
#include <atcoder/string>
#include <atcoder/twosat>

using namespace std;

int main() {
	int n;
	cin >> n;
	atcoder::dsu uf(n);
	uf.merge(2, 3);
	auto groups = uf.groups();
	int group_number = 0;
	for (auto group : groups) {
		for (auto member : group) {
			cout << "group_number is " << group_number << ", member is " << member << endl;
		}
		group_number++;
	}
	for (int i = 0;i < n;i++) {
		cout << i << "'s leader is " << uf.leader(i) << endl;
	}
}
