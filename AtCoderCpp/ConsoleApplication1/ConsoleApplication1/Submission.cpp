#include <iostream>
#include <vector>
#include <queue>
using namespace std;
using P = pair<pair<int, int>, int>;

const vector<int> dx = { 1,0,-1,0 };
const vector<int> dy = { 0,-1,0,1 };

int main() {
    int h, w;
    cin >> h >> w;
    vector<string> s(h);
    for (int i = 0;i < h;i++) {
        cin >> s[i];
    }
    int ans = 0;
    queue<P> que;
    for (int i = 0;i < h;i++) {
        for (int j = 0;j < w;j++) {
            if (s[i][j] == '#') {
                que.push(make_pair(make_pair(i, j), 1));
                ans++;
            }
        }
    }
    int phase = 1;
    auto check = [&](pair<int, int> xy) -> bool {
        auto [x, y] = xy;
        int black_cnt = 0;
        for (int i = 0;i < 4;i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (0 <= nx && nx < h && 0 <= ny && ny <= w) {
                if (s[nx][ny] == '#') {
                    black_cnt++;
                }
            }
        }
        return black_cnt == 1;
        };
    vector<pair<int, int>> masu_koho;
    while (!que.empty()) {
        auto [xy, masu_phase] = que.front();
        if (phase < masu_phase) {
            for (auto [x, y] : masu_koho) {
                s[x][y] = '#';
                ans++;
            }
            masu_koho.clear();
            phase++;
        }
        auto [x, y] = xy;
        que.pop();
        for (int i = 0;i < 4;i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (0 <= nx && nx < h && 0 <= ny && ny <= w) {
                if (s[nx][ny] == '.' && check({ nx,ny })) {
                    que.push(make_pair(make_pair(nx, ny), phase + 1));
                    masu_koho.push_back({ nx,ny });
                }
            }
        }
    }
    for (auto [x, y] : masu_koho) {
        s[x][y] = '#';
        ans++;
    }
    cout << ans << endl;
}
