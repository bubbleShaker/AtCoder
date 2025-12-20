#include <bits/stdc++.h>
#include "../ac-library/atcoder/all"
using namespace std;

int main() {
    string s;
    cin>>s;
    int i=s[0]-'0';
    int j=s[2]-'0';
    if(j!=8){
        cout<<i<<"-"<<j+1<<endl;
    }else{
        cout<<i+1<<"-"<<1<<endl;
    }
}
