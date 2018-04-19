namespace Entap.Expr
{
	/// <summary>
	/// 二項演算子を持つ式
	/// </summary>
	internal class BinaryExpr : IExpr
	{
		readonly string _op;
		readonly IExpr _left;
		readonly IExpr _right;

		/// <summary>
		/// <see cref="T:Entap.Expr.BinaryExpr"/> クラスのインスタンスを初期化する。
		/// </summary>
		/// <param name="op">演算子</param>
		/// <param name="left">左のノード</param>
		/// <param name="right">右のノード</param>
		public BinaryExpr(string op, IExpr left, IExpr right)
		{
			_op = op;
			_left = left;
			_right = right;
		}

		/// <summary>
		/// この式を評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="binding">変数のバインディング</param>
		public ExprValue Eval(BindingDelegate binding)
		{
			return ExprValue.EvalBinaryOperator(_left.Eval(binding), _right.Eval(binding), _op);
		}
	}
}
