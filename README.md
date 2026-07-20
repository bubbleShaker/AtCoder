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

リポジトリに含めるのは `atcoder/` のヘッダ・`LICENSE`・`README.md`・`expander.py` のみ。
ドキュメント（`document_ja/` `document_en/`）とテストは合計 3.9MB あり、ビルドにも提出にも
不要なため `.gitignore` で除外している。更新する場合は上流を clone し直して `atcoder/` を差し替える。

### 対応構成

x64 のみ。x86 は ac-library が 64bit 専用の `_umul128` を使っているためビルドできない
（AtCoder のジャッジは 64bit なので実用上の問題はない）。

## CodinGame

`CSharp/CSharp/CodinGame/` に置いている。
ローカル対戦用の referee は別リポジトリなので含めていない。必要なら取得する。

```
git clone https://github.com/CodinGame/Code4Life.git CSharp/CSharp/CodinGame/Code4Life
```
