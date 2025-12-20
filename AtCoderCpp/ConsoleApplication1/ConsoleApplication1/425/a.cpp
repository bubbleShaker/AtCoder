#include <execution>
#include <iostream>
using namespace std;

int main() {
    int n;
    cin >> n;
    long long ans = 0;
    for (long long i = 0;i <= n;i++) {
        ans += (i % 2 == 0 ? 1 : -1) * i * i * i;
    }
    cout << ans << endl;
}
