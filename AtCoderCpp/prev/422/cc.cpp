#include <bits/stdc++.h>
#include "../ac-library/atcoder/all"
using namespace std;
using ll=long long;

int main() {
  int t;
  cin>>t;
  while(t--){
    ll na,nb,nc;
    cin>>na>>nb>>nc;
    ll max_ans=min(na,nc);
    ll yojo=abs(na-nc);
    if(max_ans<=nb+yojo){
      cout<<max_ans<<endl;
    }else{
      cout<<(na+nb+nc)/3<<endl;
    }
  }
}
