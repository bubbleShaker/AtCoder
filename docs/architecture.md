# リポジトリ構成

AtCoder / CodinGame の提出コード置き場の構造。セットアップ手順やビルド上の注意は [README.md](../README.md) を参照。

## 全体像（パッケージ図）

```mermaid
graph TD
    root["AtCoder/"]

    root --> cpp["Cpp/<br/><i>C++ · VS2022 / MSVC v143</i>"]
    root --> cs["CSharp/<br/><i>C# · .NET 8</i>"]
    root --> py["Py/<br/><i>Python</i>"]
    root --> docs["docs/<br/><i>本ドキュメント</i>"]

    cpp --> cpp_sln["Cpp.sln<br/><i>x64 のみ</i>"]
    cpp --> cpp_proj["Cpp/<br/><i>プロジェクト本体</i>"]
    cpp --> cpp_inc["include/bits/<br/><i>bits/stdc++.h の MSVC 互換版</i>"]
    cpp --> cpp_acl["ac-library/<br/><i>AtCoder 公式ライブラリ (864245a)</i>"]

    cpp_proj --> cpp_sub["Submission.cpp<br/><b>唯一のビルド対象</b>"]
    cpp_proj --> cpp_in["input.txt<br/><i>デバッガ入力</i>"]
    cpp_proj --> cpp_cur["425/<br/><i>直近コンテストの解答</i>"]
    cpp_proj --> cpp_prev["prev/<br/><i>過去コンテストの解答・メモ</i>"]

    cs --> cs_sln["CSharp.sln"]
    cs --> cs_proj["CSharp/<br/><i>プロジェクト本体</i>"]
    cs --> cs_memo["Fetch.md / kosatu.md<br/><i>調査メモ</i>"]

    cs_proj --> cs_sub["Submission.cs<br/><b>唯一のビルド対象</b>"]
    cs_proj --> cs_cg["CodinGame/<br/><i>ボットコード（除外中）</i>"]
    cs_proj --> cs_arc["162/<br/><i>過去問（除外中）</i>"]

    py --> py_main["main.py<br/><i>定型 import のテンプレート</i>"]

    classDef build fill:#2d6a4f,stroke:#95d5b2,color:#fff
    classDef lib fill:#1d3557,stroke:#a8dadc,color:#fff
    classDef archive fill:#5c5470,stroke:#cdc4e0,color:#fff
    class cpp_sub,cs_sub build
    class cpp_inc,cpp_acl lib
    class cpp_cur,cpp_prev,cs_cg,cs_arc archive
```

緑がビルド対象、青が依存ライブラリ、紫がアーカイブ（追跡はしているがコンパイルされない）。

## ビルド依存（コンポーネント図）

C++ / C# のどちらも **「今書いている 1 ファイルだけをコンパイルし、残りはアーカイブとして置いておく」** という同じ構造になっている。
提出コードは global namespace に `Main` / `main` や `GameState` を定義する単体プログラムなので、同時にコンパイルすると必ず重複定義で衝突するため。

```mermaid
graph LR
    subgraph cppsub["C++ (Cpp.sln / x64 のみ)"]
        vcxproj["Cpp.vcxproj"]
        subm_cpp["Submission.cpp"]
        arc_cpp["425/*.cpp<br/>prev/**/*.cpp"]
        bits["include/bits/stdc++.h"]
        acl["ac-library/atcoder/*.hpp"]

        vcxproj -->|"ClCompile"| subm_cpp
        vcxproj -.->|"未登録（アーカイブ）"| arc_cpp
        vcxproj -->|"AdditionalIncludeDirectories"| bits
        vcxproj -->|"AdditionalIncludeDirectories"| acl
        subm_cpp -->|"include bits/stdc++.h"| bits
        subm_cpp -.->|"include atcoder/all"| acl
    end

    subgraph cssub["C# (.NET 8)"]
        csproj["CSharp.csproj"]
        subm_cs["Submission.cs"]
        arc_cs["CodinGame/*.cs<br/>162/a.cs"]
        nuget["NuGet: ac-library-csharp 3.9.2"]
        referee["CodinGame/Code4Life/<br/><i>外部 repo・gitignore</i>"]

        csproj -->|"Compile"| subm_cs
        csproj -.->|"Compile Remove"| arc_cs
        csproj -->|"PackageReference"| nuget
        csproj -.->|"Compile Remove"| referee
    end

    classDef build fill:#2d6a4f,stroke:#95d5b2,color:#fff
    classDef lib fill:#1d3557,stroke:#a8dadc,color:#fff
    classDef archive fill:#5c5470,stroke:#cdc4e0,color:#fff
    class subm_cpp,subm_cs build
    class bits,acl,nuget lib
    class arc_cpp,arc_cs,referee archive
```

実線が有効な依存、破線が意図的に切ってある依存。

### 図から読み取れる制約

| 制約 | 理由 |
|---|---|
| ビルド対象は `Submission.cpp` / `Submission.cs` の 1 つだけ | 提出コードは単体プログラムなので同時コンパイルすると重複定義で衝突する。切り替える時は `Cpp.vcxproj` の `ClCompile` / `CSharp.csproj` の `Compile Remove` を入れ替える |
| C++ の構成は x64 のみ | ac-library が 64bit 専用の組み込み関数 `_umul128` を使っており x86 ではビルドできない。AtCoder のジャッジも 64bit |
| `bits/stdc++.h` と ac-library は repo 同梱 | MSVC のインストールフォルダにコピーする方式だと VS 更新でパスのバージョン番号が変わって消えるため |
| `Code4Life/` は追跡しない | CodinGame 公式 referee の別リポジトリ。手元にあるかどうかでビルド結果が変わらないよう `Compile Remove` もしている |

## 提出コードのライフサイクル

```mermaid
stateDiagram-v2
    [*] --> 作業中: 新しい問題に着手
    作業中 --> 作業中: input.txt でデバッグ実行
    作業中 --> 提出済み: AtCoder / CodinGame へ提出
    提出済み --> アーカイブ: コンテスト番号のフォルダへ退避
    アーカイブ --> [*]

    note right of 作業中
        Submission.cpp / Submission.cs
        ビルド対象はここだけ
    end note

    note right of アーカイブ
        Cpp: 425/ → prev/424/ → prev/422/
        C#: CodinGame/ · 162/
        追跡はするがコンパイルしない
    end note
```
