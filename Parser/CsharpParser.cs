using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ACQC.Metrics {
	internal class CsharpParser : BaseParser {
		private TokenClass _lastTokenClass;
		private String _lastToken;
		private Int32 _braceDepth;
		private Int32 _parenthesisDepth;
		private Int32 _argumentCount;
		private ParserState _state;
		private Statistics _statistics;
		private bool blockComment = false;
		private bool multiLineMacro = false;


		public CsharpParser (FileInfo inputFile)
			: base (inputFile)
		{
			_braceDepth = 0;
			_parenthesisDepth = 0;
			_state = ParserState.START;
			_lastToken = String.Empty;
			_lastTokenClass = TokenClass.Identifier;
		}

		private static readonly String tokenEx = @"([\w\.:~/]+)|[;,\(\)=\{\}]";
		private static readonly String lineComment = @"//.*";
		private static readonly String innerComment = @"/\*.*?\*/";
		private static readonly String startComment = @"/\*.*";
		private static readonly String endComment = @".*\*/";
		private static readonly String stringLiteral = @"""(\\""|[^""])*""";
		private static readonly String charLiteral = @"'[^\']'";

		// Removes all comments and literal strings from the line
		private static String StripLine (String line, ref Boolean blockComment)
		{
			String l = line;
			l = Regex.Replace (l, stringLiteral, String.Empty);
			l = Regex.Replace (l, charLiteral, String.Empty);

			if (!blockComment) {
				l = Regex.Replace (l, lineComment, String.Empty);
				l = Regex.Replace (l, innerComment, String.Empty);
				if (l.Contains ("/*")) {
					l = Regex.Replace (l, startComment, String.Empty);
					blockComment = true;
				}
			} else {
				if (l.Contains ("*/")) {
					l = Regex.Replace (l, innerComment, String.Empty);
					l = Regex.Replace (l, endComment, String.Empty);
					blockComment = false;
					l = Regex.Replace (l, lineComment, String.Empty);
					if (l.Contains ("/*")) {
						l = Regex.Replace (l, startComment, String.Empty);
						blockComment = true;
					}
				} else {
					l = String.Empty;
				}
			}

			return l;
		}

		public override void ParseLine (String line)
		{
			_collector.IncrementLINES ();

			// Trim the line of all whitespace
			String trimmedLine = line.Trim ();

			// if everything was removed, this was a line of whitespaces
			if (String.IsNullOrEmpty (trimmedLine)) {
				_collector.IncrementLLOW ();
			} else {

				String strippedLine = StripLine (line, ref blockComment);

				//if (strippedLine.Contains("CPPUNIT_TEST_SUITE_REGISTRATION"))
				//{
				//    //testFile = true;
				//    // TODO this information is not used further
				//}

				if (trimmedLine.Length > 2) {
					String result = Regex.Replace (line, stringLiteral, String.Empty);
					if (result.Length > strippedLine.Length) {
						// Some comment was present in the line and removed
						_collector.IncrementLLOCi ();
					}
					if (strippedLine.Length > 2) {
						// Some code is left
						//std::cout << line << std::endl;
						_collector.IncrementLLOC ();
					}
				}

				// Macros are ignored
				if (!strippedLine.Contains ('#') && !multiLineMacro) {
					ParseString (strippedLine);
				} else {
					multiLineMacro = strippedLine.EndsWith (@"\");
				}
			}
		}

		private void ParseString (String line)
		{
			MatchCollection results = Regex.Matches (line, tokenEx);
			foreach (Match match in results) {
				ProcessToken (match.Value);
			}
		}

		private void ProcessToken (String token)
		{
			TokenClass tclass = getTokenClass (token);
			//std::cout << collector.getCurrentLine() << "\t" << token << "\t"
			// << state << "\t" << lastToken << "\t" << depth << std::endl;

			switch (_state) {
			case ParserState.START:
				ParseTokenStart (token, tclass);
				break;
			case ParserState.OpenParenthesis:
				ParseTokenOpenParenthesis (tclass);
				break;
			case ParserState.CloseParenthesis:
				ParseTokenCloseParenthesis (token, tclass);
				break;
			case ParserState.CppFunction:
				ParseTokenCppFunction (token, tclass);
				break;
			case ParserState.Function:
				ParseTokenCFunction (token, tclass);
				break;
			case ParserState.Expression:
				ParseTokenExpression (token, tclass);
				break;
			}
		}

		private void ParseTokenExpression (String token, TokenClass tclass)
		{
			if (tclass == TokenClass.Semicolon) {
				_state = ParserState.START;
				_lastTokenClass = tclass;
				_lastToken = token;
			}
		}

		private void ParseTokenCFunction (String token, TokenClass tclass)
		{
			switch (tclass) {
			case TokenClass.LeftBrace:
				_state = ParserState.START;
				_braceDepth++;
				_statistics.countProcs++;
				_collector.FunctionEnter (_lastToken, _argumentCount);
				_lastTokenClass = tclass;
				_lastToken = token;
				break;
			case TokenClass.Operation:
				_state = ParserState.START;
				_lastTokenClass = tclass;
				_lastToken = token;
				break;
			}
		}

		private void ParseTokenCppFunction (String token, TokenClass tclass)
		{
			switch (tclass) {
			case TokenClass.LeftBrace:
				_state = ParserState.START;
				_braceDepth++;
				_statistics.countProcs++;
				_collector.FunctionEnter (_lastToken, _argumentCount);
				_lastTokenClass = tclass;
				_lastToken = token;
				break;
			case TokenClass.Semicolon:
				_state = ParserState.START;
				_lastTokenClass = tclass;
				_lastToken = token;
				break;
			}
		}

		private void ParseTokenCloseParenthesis (String token, TokenClass tclass)
		{
			switch (tclass) {
			case TokenClass.LeftParenthesis:
				_state = ParserState.OpenParenthesis;
				break;
			case TokenClass.Semicolon:
			case TokenClass.Colon:
				_state = ParserState.START;
				_lastTokenClass = tclass;
				_lastToken = token;
				break;
			case TokenClass.LeftBrace:
				_state = ParserState.START;
				_braceDepth++;
				if (_lastTokenClass == TokenClass.Identifier) {	// function found
					_statistics.countProcs++;
					_collector.FunctionEnter (_lastToken, _argumentCount);
				} else {
					_collector.DepthMore ();
				}
				_lastTokenClass = tclass;
				_lastToken = token;
				break;
			case TokenClass.K_const:
			case TokenClass.K_volatile:
			case TokenClass.K_throw:
				_state = ParserState.CppFunction;
				break;
			default:
				if (_lastTokenClass == TokenClass.Identifier &&
					_braceDepth == 0 &&
					_lastToken != "__declspec") // Fix
                    {
					_state = ParserState.Function;
				} else {
					_state = ParserState.START;
					_lastTokenClass = tclass;
					_lastToken = token;
				}
				break;
			}
		}

		private void ParseTokenOpenParenthesis (TokenClass tclass)
		{
			switch (tclass) {
			case TokenClass.Colon:
				if (_parenthesisDepth == 0) {
					_argumentCount++;
				}
				break;
			case TokenClass.LeftParenthesis:
				_parenthesisDepth++;
				break;
			case TokenClass.RightParenthesis:
				if (_parenthesisDepth == 0) {
					_state = ParserState.CloseParenthesis;
				} else {
					_parenthesisDepth--;
				}
				break;
			default:
				if (tclass != TokenClass.T_void) {
					if (_argumentCount == 0) {
						_argumentCount = 1;
					}
				}
				break;
			}
		}

		private void ParseTokenStart (String token, TokenClass tclass)
		{
			switch (tclass) {
			case TokenClass.LeftBrace:
				if (_braceDepth > 0) {
					if (_lastTokenClass == TokenClass.K_try) {
						_statistics.countTry++;
					} else if (_lastTokenClass == TokenClass.K_do) {
						_statistics.countCCx += _braceDepth;
						_collector.Construct ();

						_statistics.countDo++;
					}
					_braceDepth++;
					_collector.DepthMore ();
				}
				// else must be start of toplevel namespace or class construct	
				break;
			case TokenClass.RightBrace:
				if (_braceDepth == 1) { // end of function
					_braceDepth--;
					_collector.FunctionLeave ();
				} else if (_braceDepth > 1) {
					_braceDepth--;
					_collector.DepthLess ();
				}
				// else must be end of toplevel namespace or class construct
				break;
			case TokenClass.LeftParenthesis:
				_state = ParserState.OpenParenthesis;
				_argumentCount = 0;
				_parenthesisDepth = 0;
				switch (_lastTokenClass) {
				case TokenClass.K_if:
					_collector.Construct ();
					_statistics.countIf++;
					_statistics.countCCx += _braceDepth;
					break;
				case TokenClass.K_for:
					_collector.Construct ();
					_statistics.countFor++;
					_statistics.countCCx += _braceDepth;
					break;
				case TokenClass.K_while:
					_collector.Construct ();
					_statistics.countWhile++;
					_statistics.countCCx += _braceDepth;
					break;
				case TokenClass.K_switch:
					_statistics.countSwitch++;
					break;
				case TokenClass.K_catch:
					_collector.Construct ();
					_statistics.countCatch++;
					_statistics.countCCx += _braceDepth;
					break;
				default:
					break;
				}
				break;
			case TokenClass.K_case:
				_collector.Construct ();
				_statistics.countCase++;
				_statistics.countCCx += _braceDepth;
				break;
			case TokenClass.Operation:
				_state = ParserState.Expression;
				break;
			default:
				_lastTokenClass = tclass;
				_lastToken = token;
				break;
			}
		}


		public Statistics getStatistics ()
		{
			_statistics.countCC = _statistics.countDo + _statistics.countIf + _statistics.countFor + _statistics.countProcs +
				_statistics.countCase + _statistics.countCatch + _statistics.countWhile;
			return _statistics;
		}

		public TokenClass getTokenClass (String token)
		{
			TokenClass result = TokenClass.Identifier;
			switch (token) {
			case "...":
				result = TokenClass.Ellipsis;
				break;
			case ";":
				result = TokenClass.Semicolon;
				break;
			case ",":
				result = TokenClass.Colon;
				break;
			case "(":
				result = TokenClass.LeftParenthesis;
				break;
			case ")":
				result = TokenClass.RightParenthesis;
				break;
			case "{":
				result = TokenClass.LeftBrace;
				break;
			case "}":
				result = TokenClass.RightBrace;
				break;

			case "void":
				result = TokenClass.T_void;
				break;

			case "if":
				result = TokenClass.K_if;
				break;
			case "for":
				result = TokenClass.K_for;
				break;
			case "while":
				result = TokenClass.K_while;
				break;
			case "do":
				result = TokenClass.K_do;
				break;
			case "switch":
				result = TokenClass.K_switch;
				break;
			case "case":
				result = TokenClass.K_case;
				break;

			case "try":
			case "__try":
				result = TokenClass.K_try;
				break;
			case "catch":
			case "__except":
				result = TokenClass.K_catch;
				break;
			case "throw":
				result = TokenClass.K_throw;
				break;

			case "class":
				result = TokenClass.K_class;
				break;
			case "enum":
				result = TokenClass.K_enum;
				break;
			case "struct":
				result = TokenClass.K_struct;
				break;
			case "union":
				result = TokenClass.K_union;
				break;

			case "const":
				result = TokenClass.K_const;
				break;
			case "volatile":
				result = TokenClass.K_volatile;
				break;
			case "extern":
				result = TokenClass.K_extern;
				break;
			case "mutable":
				result = TokenClass.K_mutable;
				break;
			case "register":
				result = TokenClass.K_register;
				break;
			case "restrict":
				result = TokenClass.K_restrict;
				break;

			case "__cdecl":
				result = TokenClass.Call_cdecl;
				break;
			case "__stdcall":
				result = TokenClass.Call_stdcall;
				break;
			case "__fastcall":
				result = TokenClass.Call_fastcall;
				break;

			case "=":
				result = TokenClass.Operation;
				break;
			}
			return result;
		}
	}
}
