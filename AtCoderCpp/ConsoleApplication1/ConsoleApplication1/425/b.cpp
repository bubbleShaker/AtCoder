#include <execution>
#include <iostream>
using namespace std;

int main() {
    int n;
    cin >> n;
    vector<int> a(n),p(n);
    for (int i = 0;i < n;i++) {
        cin >> a[i];
        p[i] = i + 1;
    }
    do {
        bool is_ok = true;
        for (int i = 0;i < n;i++) {
            if (a[i] != -1 && a[i] != p[i]) {
                is_ok = false;
            }
        }
        if (is_ok) {
            cout << "Yes" << endl;
            for (int i = 0;i < n;i++) {
                cout << p[i] << " ";
            }
            return 0;
        }
    } while (next_permutation(p.begin(), p.end()));
    cout << "No" << endl;
}
