using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ACQC.Metrics
{
	internal abstract class BaseParser: IParser
	{

		protected ResultCollector _collector;
		private FileInfo _file = null;

		public BaseParser(FileInfo inputFile)
		{
			_collector = new ResultCollector(inputFile);
			_file = inputFile;
			EnterState(Parser.State.Initial);
		}

		#region State management

		private Stack<Parser.State> _states = new Stack<Parser.State>();

		protected void EnterState(Parser.State state)
		{
			_states.Push(state);
		}

		protected Boolean InState(Parser.State state)
		{
			return _states.Peek() == state;
		}

		protected Parser.State LeaveState()
		{
			return _states.Pop();
		}

		#endregion

		public ResultCollector Results
		{
			get
			{
				return _collector;
			}
		}

		public abstract void ParseText(TextReader line);

		public virtual void ParseFile()
		{
			using (Stream fileStream = new FileStream(_file.FullName, FileMode.Open, FileAccess.Read))
			{
				TextReader input = new StreamReader(fileStream);
				ParseText(input);
			}
		}
	}
}
