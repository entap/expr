using System;

namespace Entap.Expr
{
	[System.Serializable]
	public class ExprSyntaxException : Exception
	{
		readonly int _offset;

		/// <summary>
		/// 数式の中の位置
		/// </summary>
		public int Offset { get => _offset; }

		/// <summary>
		/// <see cref="T:ExprSyntaxException"/> クラスのインスタンスを初期化する。
		/// </summary>
		public ExprSyntaxException()
		{
		}

		/// <summary>
		/// <see cref="T:ExprSyntaxException"/> クラスのインスタンスを初期化する。
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="message">オフセット</param>
		public ExprSyntaxException(string message, int offset) : base(message)
		{
			_offset = offset;
		}
	}
}
