#include <bits/stdc++.h>
#include "../ac-library/atcoder/all"
using namespace std;

int main() {
  int n,m,k;
  cin>>n>>m>>k;
  vector<set<int>> set_vec(n);
  for(int i=0;i<k;i++){
    int a,b;
    cin>>a>>b;
    a--;
    set_vec[a].insert(b);
    if(set_vec[a].size()==m){
      cout<<a+1<<" ";
    }
  }
}
