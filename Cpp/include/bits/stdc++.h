// bits/stdc++.h — MSVC 互換版
//
// 本来 <bits/stdc++.h> は GCC (libstdc++) の内部ヘッダで、MSVC には存在しない。
// AtCoder のジャッジは GCC なので提出コードでは使えるが、手元の Visual Studio では
// 通らないため、同名のヘッダを自前で用意して include パスに通している。
//
// 中身は「MSVC に存在する標準ヘッダを全部 include する」だけ。
// libstdc++ 版をコピーすると GCC 固有ヘッダ (<ext/*>, <cxxabi.h> 等) が混ざって壊れるので、
// MSVC の include ディレクトリに実在するものだけを列挙している。

#pragma once

// _HAS_CXX20 / _HAS_CXX23 を得るために最初に読む。
// これらは MSVC が「今どの言語標準でコンパイル中か」を教えてくれるマクロで、
// その標準でまだ使えないヘッダを include してしまわないよう条件分岐するために使う。
// （構成の /std 設定を下げた場合でも、この分岐があれば警告まみれにならない）
#include <version>

// ---- C 由来ヘッダ ----
#include <cassert>
#include <cctype>
#include <cerrno>
#include <cfenv>
#include <cfloat>
#include <cinttypes>
#include <climits>
#include <clocale>
#include <cmath>
#include <csetjmp>
#include <csignal>
#include <cstdarg>
#include <cstddef>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <ctime>
#include <cuchar>
#include <cwchar>
#include <cwctype>

// ---- コンテナ ----
#include <array>
#include <bitset>
#include <deque>
#include <forward_list>
#include <list>
#include <map>
#include <queue>
#include <set>
#include <stack>
#include <unordered_map>
#include <unordered_set>
#include <vector>

// ---- アルゴリズム・ユーティリティ ----
#include <algorithm>
#include <functional>
#include <iterator>
#include <memory>
#include <numeric>
#include <random>
#include <ratio>
#include <scoped_allocator>
#include <tuple>
#include <type_traits>
#include <typeindex>
#include <typeinfo>
#include <utility>

// ---- 文字列・入出力 ----
#include <fstream>
#include <iomanip>
#include <ios>
#include <iosfwd>
#include <iostream>
#include <istream>
#include <locale>
#include <ostream>
#include <regex>
#include <sstream>
#include <streambuf>
#include <string>

// ---- 数値・診断・その他 ----
#include <chrono>
#include <complex>
#include <exception>
#include <initializer_list>
#include <limits>
#include <new>
#include <stdexcept>
#include <system_error>
#include <valarray>

// ---- 並行 ----
#include <atomic>
#include <condition_variable>
#include <future>
#include <mutex>
#include <shared_mutex>
#include <thread>

#if _HAS_CXX17
#include <any>
#include <charconv>
#include <execution>
#include <filesystem>
#include <memory_resource>
#include <optional>
#include <string_view>
#include <variant>
#endif // _HAS_CXX17

#if _HAS_CXX20
#include <barrier>
#include <bit>
#include <compare>
#include <concepts>
#include <coroutine>
#include <format>
#include <latch>
#include <numbers>
#include <ranges>
#include <semaphore>
#include <source_location>
#include <span>
#include <stop_token>
#include <syncstream>
#endif // _HAS_CXX20

#if _HAS_CXX23
#include <expected>
#include <generator>
#include <mdspan>
#include <print>
#include <spanstream>
#include <stacktrace>
#include <stdfloat>
#endif // _HAS_CXX23
