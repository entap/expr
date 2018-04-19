namespace Entap.Expr
{
	/// <summary>
	/// 単項演算子を持つ式
	/// </summary>
	internal class UnaryExpr : IExpr
	{
		readonly string _op;
		readonly IExpr _expr;

		/// <summary>
		/// <see cref="T:Entap.Expr.UnaryExpr"/> クラスのインスタンスを初期化する。
		/// </summary>
		/// <param name="op">演算子</param>
		/// <param name="expr">式</param>
		public UnaryExpr(string op, IExpr expr)
		{
			_op = op;
			_expr = expr;
		}

		/// <summary>
		/// この式を評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="binding">変数のバインディング</param>
		public ExprValue Eval(BindingDelegate binding)
		{
			return ExprValue.EvalUnaryOperator(_expr.Eval(binding), _op);
		}
	}
}
