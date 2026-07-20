#include <bits/stdc++.h>
#include <atcoder/all>

using namespace std;
using namespace atcoder;
using ll = long long;

int main() {
	ll n, m;
	cin >> n >> m;
	vector<ll> a(n), b(n - 1);
	for (int i = 0;i < n;i++) {
		cin >> a[i];
	}
	for (int i = 0;i < n-1;i++) {
		cin >> b[i];
	}
	ll ans = 100100100;

	for (int first = 0;first < 2;first++) {
		vector<int> kouho(n);
		kouho[0] = first;
		ll cnt = 0;
		for (int i = 0;i < n - 1;i++) {
			if (b[i] == 0) {
				if (kouho[i] == 0) {
					kouho[i + 1] = 0;
				}
				else {
					kouho[i + 1] = 1;
				}
			}
			if (b[i] == 1) {
				if (kouho[i] == 0) {
					kouho[i + 1] = 1;
				}
				else {
					kouho[i + 1] = 0;
				}
			}
		}
		for (int i = 0;i < n;i++) {
			cnt += abs(kouho[i] - a[i]);
		}
		ans = min(ans, cnt);
	}
	cout << ans << endl;
	return 0;
}