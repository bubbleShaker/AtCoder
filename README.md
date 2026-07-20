# AtCoder

AtCoder / CodinGame の提出コード置き場。

## ディレクトリ

| パス | 内容 |
|---|---|
| `Cpp/` | C++ (Visual Studio 2022 / MSVC v143)。メインの提出環境 |
| `CSharp/` | C# (.NET 8) |
| `Py/` | Python |

## Cpp のセットアップ

`Cpp/Cpp.sln` を開くだけでビルドできる。追加の手動セットアップは不要。

### インクルードパス

`Cpp/Cpp/Cpp.vcxproj` の `AdditionalIncludeDirectories` で全 4 構成に以下を通してある。

| パス | 用途 |
|---|---|
| `Cpp/include/` | `<bits/stdc++.h>` の MSVC 互換版。GCC 固有のこのヘッダを手元でも使うため |
| `Cpp/ac-library/` | AtCoder 公式ライブラリ。`<atcoder/all>` を解決する |

どちらもリポジトリに含めてあるので、clone すればそのままビルドできる。
MSVC のインストールフォルダに手動コピーする方式は、Visual Studio を更新すると
パスのバージョン番号が変わって消えるため採っていない。

### ac-library について

上流 https://github.com/atcoder/ac-library の **`864245a`**（`Merge pull request #186 from atcoder/opt/dsu`）
時点のヘッダを取り込んでいる。

ライセンスは CC0 1.0 Universal（パブリックドメイン相当。帰属表示の義務は無いが `LICENSE` を同梱している）。

リポジトリに含めるのは `atcoder/` のヘッダ・`LICENSE`・`README.md`・`expander.py` のみ。
ドキュメント（`document_ja/` `document_en/`）とテストは合計 3.9MB あり、ビルドにも提出にも不要。

`.gitignore` は**ホワイトリスト方式**にしてある。上流を clone し直して差し替えた時に、
新しいディレクトリやメタファイルが増えていても黙って追跡対象に入らないようにするため。
更新する場合は上流を clone し直して `atcoder/` を差し替えればよい。

### 対応構成

**x64 のみ。** x86 は ac-library が 64bit 専用の組み込み関数 `_umul128` を使っているため
ビルドできないので、選び間違えないよう sln の構成一覧から削除してある
（AtCoder のジャッジは 64bit なので実用上の問題はない）。

## CSharp のビルド対象

CodinGame / AtCoder の提出コードはどれも global namespace に `Main` や `GameState` を
定義する単体プログラムなので、同時にコンパイルすると必ず重複定義で衝突する。

そのため `CSharp/CSharp/CSharp.csproj` で「今書いているもの 1 つだけ」をビルド対象にし、
残りは `Compile Remove` している。**現在の対象は `Submission.cs`。**
別のものに切り替える時は csproj の `Compile Remove` を入れ替える。

## CodinGame

`CSharp/CSharp/CodinGame/` に置いている。
ローカル対戦用の referee は別リポジトリなので含めていない。必要なら取得する。

```
git clone https://github.com/CodinGame/Code4Life.git CSharp/CSharp/CodinGame/Code4Life
```
