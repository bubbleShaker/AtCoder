#include <bits/stdc++.h>
#include "../ac-library/atcoder/all"
using namespace std;

vector<int> di={-1,0,1,0};
vector<int> dj={0,1,0,-1};

int main() {
  int h,w;
  cin>>h>>w;
  vector<string> s(h);
  for(int i=0;i<h;i++){
    cin>>s[i];
  }
  for(int i=0;i<h;i++){
    for(int j=0;j<w;j++){
      if(s[i][j]!='#'){
        continue;
      }
      int black_cnt=0;
      for(int k=0;k<4;k++){
        int ni=i+di[k];
        int nj=j+dj[k];
        if(0<=ni&&ni<h&&0<=nj&&nj<w){
          if(s[ni][nj]=='#'){
            black_cnt++;
          }
        }
      }
      if(black_cnt==0||black_cnt%2){
        cout<<"No"<<endl;
        return 0;
      }
    }
  }
  cout<<"Yes"<<endl;
  return 0;
}
