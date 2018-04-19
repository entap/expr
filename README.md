# Expr
## 概要
文字列で書かれた式の評価を行うクラスです。
ゲーム用のスクリプト言語の式計算、テキスト入力欄で計算式を入力するなどに使うことを想定しています。
- System.Linq.Expressionを使っていないので、iOSでも動きます。
- ほどほど軽量

## 使用例
文字列で計算する。

    Expr.Expression.Evaluate<double>("1+2+3*4*(5+6)/7");

変数を使う。

    var vars = new Dictionary<string, object>();
    vars["x"]= 123;
    Expr.Expression.Evaluate<double>("1+x", vars);

関数を使う。

    Expr.Expression.Evaluate<double>("sin(3)");

## 演算子

|演算子の種類|結合性|演算子|
|---|---|---|
|グループ化|n/a|( … )|
|関数呼び出し|左から右|…(…)|
|論理|NOT|右から左|!…|
|ビットごとのNOT|右から左|~…|
|単項の+|右から左|+…|
|単項の-|右から左|-…|
|べき乗|右から左|…**…|
|乗算|左から右|…*…|
|除算|左から右|…/…|
|モジュロ (剰余)|左から右|…%…|
|加算|左から右|…+…|
|減算|左から右|…-…|
|左ビットシフト|左から右|…<<…|
|右ビットシフト|左から右|…>>…|
|より小さい|左から右|…<…|
|より小さいまたは等しい|左から右|…<=…|
|より大きい|左から右|…>…|
|より大きいまたは等しい|左から右|…>=…|
|等しい|左から右|…==…|
|等しくない|左から右|…!=…|
|ビットごとの AND|左から右|…&…|
|ビットごとの XOR|左から右|…^…|
|ビットごとの OR|左から右|…｜…|
|論理 AND|左から右|…&&…|
|論理 OR|左から右|…｜｜…|

## 定数

true

false

Epsilon 実数型の最も細かい値

Infinity 無限

NaN

E 自然対数

LN10

LN2

LOG10E

LOG2E

PI

SQRT1_2

SQRT2

Rad2Deg

Deg2Rad

## 標準関数

abs

acos

asin

atan

atan2

ceil

cbrt

cos

cosh

exp

floor

hypot

log

log10

log2

max

min

pow

round

sign

sinn

sinh

sqrt

tan

tanh

sq

constrain

mag

dist

lerp

norm

map
