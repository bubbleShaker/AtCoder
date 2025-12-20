#include <bits/stdc++.h>
#include "../ac-library/atcoder/all"
using namespace std;

int main() {
  int n;
  cin>>n;
  vector<pair<int,int>> ab(n+1);
  vector<vector<int>> g(n+1);
  for(int i=1;i<=n;i++){
    cin>>ab[i].first>>ab[i].second;
  }
  queue<int> que;
  set<int> check_set;
  for(int i=1;i<=n;i++){
    auto [a,b] = ab[i];
    if(a==b&&a==0){
      que.push(i);
      check_set.insert(i);
    }else{
      g[a].push_back(i);
      g[b].push_back(i);
    }
  }
  int ans=0;
  while(!que.empty()){
    int u=que.front();//uは取得済みのスキル
    que.pop();
    ans++;
    for(auto v:g[u]){
      if(check_set.count(v))continue;//すでに取得済みのスキルの場合はpushしない
      que.push(v);   
      check_set.insert(v);
    }
  }
  cout<<ans<<endl;
}
