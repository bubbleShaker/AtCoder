#include <execution>
#include <iostream>
using namespace std;

int main() {
    int n, q;
    cin >> n >> q;
    vector<long long> a(n * 2 + 1), rsw(n * 2 + 1);
    for (int i = 1;i <= n;i++) {
        cin >> a[i];
        a[i + n] = a[i];
    }
    for (int i = 1;i <= 2 * n;i++) {
        rsw[i] = rsw[i - 1] + a[i];
    }
    int itr = 0;
    while (q--) {
        int t;
        cin >> t;
        if (t == 1) {
            int c;
            cin >> c;
            itr += c;
            if (itr > n) {
                itr %= n;
            }
        }
        if (t == 2) {
            int l, r;
            cin >> l >> r;
            cout << rsw[itr + r] - rsw[itr + l - 1] << endl;
        }
    }
}
