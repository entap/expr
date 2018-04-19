# Expr
## 概要
文字列で書かれた式の評価を行うクラスです。
ゲーム用のスクリプト言語の式計算、テキスト入力欄で計算式を入力するなどに使うことを想定しています。
- System.Linq.Expressionを使っていないので、iOSでも動きます。
- ほどほど軽量

## 使用例
文字列で計算する。
    Expr.Expression.Eval<double>("1+2+3*4*(5+6)/7");
変数を使う。
    var vars = new Dictionary<string, object>();
    vars["x"]= 123;
    Expr.Expression.Eval<double>("1+x", vars);
関数を使う。
    Expr.Expression.Eval<double>("sin(3)");

## 演算子

## 標準関数
