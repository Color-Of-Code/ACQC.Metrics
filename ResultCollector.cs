using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ACQC.Metrics {
	public class ResultCollector : IXmlSerializable {
		public ResultCollector (FileInfo file)
		{
			_fileMetrics = new Data.Metrics (file.FullName, file.Name);
			_functionMetrics = new List<Data.Metrics> ();
			_currentMetrics = null;
			_outsideComments = 0;
			Depth = 0;
		}

		public void IncrementLLOC ()
		{
			_fileMetrics.IncrementLLOC (1);
			if (_currentMetrics != null) {
				_currentMetrics.IncrementLLOC (1);
			}
		}

		public void IncrementLLOCi ()
		{
			_fileMetrics.IncrementLLOCi (1);
			if (_currentMetrics != null) {
				_currentMetrics.IncrementLLOCi (1);
			} else {	// The comments are counted towards the following function
				_outsideComments++;
			}
		}

		public void IncrementLLOW ()
		{
			_fileMetrics.IncrementLLOW (1);
			if (_currentMetrics != null) {
				_currentMetrics.IncrementLLOW (1);
			} else {
				_outsideComments = 0;
			}
		}

		public void IncrementLINES ()
		{
			_fileMetrics.IncrementLINES (1);
			if (_currentMetrics != null) {
				_currentMetrics.IncrementLINES (1);
			}
		}

		public int CurrentLine
		{
			get
			{
				return _fileMetrics.LINES;
			}
		}

		public void FunctionEnter (String name, int parameterCount)
		{
			_fileMetrics.IncrementPROCS (1);
			_fileMetrics.IncrementCARGS (parameterCount);
			Data.Metrics function = new Data.Metrics (_fileMetrics.Filename, name, _fileMetrics.LINES);
			function.IncrementCARGS (parameterCount);
			function.IncrementCC (1);
			function.IncrementLLOCi (_outsideComments);
			_functionMetrics.Add (function);
			_currentMetrics = function;
			Depth = 1;

			_fileMetrics.IncrementCC (1);
		}

		public void FunctionLeave ()
		{
			_currentMetrics = null;
			_outsideComments = 0;
			Depth = 0;
		}

		public void DepthMore ()
		{
			Depth++;
		}

		public void DepthLess ()
		{
			Depth--;
		}

		public void Construct ()
		{
			_fileMetrics.IncrementCC (1);
			_fileMetrics.IncrementDC (Depth);
			if (_currentMetrics != null) {
				_currentMetrics.IncrementCC (1);
				_currentMetrics.IncrementDC (Depth);
			}
		}

		public Data.Metrics FileMetrics
		{
			get
			{
				return _fileMetrics;
			}
		}

		public IList<Data.Metrics> FunctionMetrics
		{
			get
			{
				return _functionMetrics;
			}
		}

		private Data.Metrics _fileMetrics;
		private List<Data.Metrics> _functionMetrics;

		private Data.Metrics _currentMetrics;
		private Int32 _outsideComments;

		private Int32 Depth
		{ get; set; }


		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema ()
		{
			return null;
		}

		public void ReadXml (System.Xml.XmlReader reader)
		{
			reader.ReadStartElement ("Metrics");
			reader.ReadStartElement ("File");
			_fileMetrics.ReadXml (reader);
			reader.ReadEndElement ();

			reader.ReadStartElement ("Functions");
			while (reader.IsStartElement ()) {
				Data.Metrics functionMetrics = new Data.Metrics ();
				reader.ReadStartElement ("Function");
				functionMetrics.ReadXml (reader);
				reader.ReadEndElement ();
				_functionMetrics.Add (functionMetrics);
			}
			reader.ReadEndElement ();
			reader.ReadEndElement ();
		}

		public void WriteXml (System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement ("Metrics");
			if (_fileMetrics.Filename != null)
				writer.WriteAttributeString ("File", _fileMetrics.Filename);
			writer.WriteStartElement ("File");
			_fileMetrics.WriteXml (writer);
			writer.WriteEndElement ();

			writer.WriteStartElement ("Functions");
			foreach (Data.Metrics metrics in _functionMetrics) {
				writer.WriteStartElement ("Function");
				metrics.WriteXml (writer);
				writer.WriteEndElement ();
			}
			writer.WriteEndElement ();

			writer.WriteEndElement ();
		}

		#endregion
	}
}
