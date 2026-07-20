#include <bits/stdc++.h>
#include "../ac-library/atcoder/all"
using namespace std;

int main() {
  vector<int> a(3);
  for(int i=0;i<3;i++)cin>>a[i];
  sort(a.begin(),a.end());
  if(a[0]==a[1]&&a[0]+a[1]>a[2]){
    cout<<"Yes"<<endl;
  }else if(a[1]==a[2]&&a[1]+a[2]>a[0]){
    cout<<"Yes"<<endl;
  }else{
    cout<<"No"<<endl;
  }
}
