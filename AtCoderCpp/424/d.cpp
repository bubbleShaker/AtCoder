#include <bits/stdc++.h>
#include "../ac-library/atcoder/all"
using namespace std;

int main() {
  vector<int> dx={0,0,1,1};
  vector<int> dy={0,1,1,0};
  int T;
  cin>>T;
  while(T--){
    int mi=(int)1e8;
    int h,w;
    cin>>h>>w;
    vector<string> s(h);
    for(int i=0;i<h;i++)cin>>s[i];
    set<vector<string>> st;
    auto dfs=[&](auto dfs,vector<string> t_s,int cnt)->void{
      bool flag=false;
      for(int x=0;x<h;x++){
        for(int y=0;y<w;y++){
          int zero_cnt=0;
          for(int i=0;i<4;i++){
            int nx=x+dx[i];
            int ny=y+dy[i];
            if(0<=nx&&nx<h&&0<=ny&&ny<w){
              if(t_s[nx][ny]=='#'){
                zero_cnt++;
              }
            }
          }
          if(zero_cnt<4)continue;
          flag=true;
          for(int i=0;i<4;i++){
            int nx=x+dx[i];
            int ny=y+dy[i];
            if(0<=nx&&nx<h&&0<=ny&&ny<w){
              if(t_s[nx][ny]=='#'){
                t_s[nx][ny]='.';
                if(!st.count(t_s)){
                  dfs(dfs,t_s,cnt+1);
                  st.insert(t_s);
                }
                t_s[nx][ny]='#';
              }
            }
          }
        }
      }
      if(!flag)mi=min(mi,cnt);
    };
    dfs(dfs,s,0);
    cout<<mi<<endl;
  }
}
