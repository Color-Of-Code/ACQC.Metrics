using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ACQC.Metrics.Data
{

    public class Metrics : IXmlSerializable, IKiviatElement
    {
        public Metrics(String filename, String name, Int32 position)
        {
            Name = name;
            Filename = filename;
            Position = position;
        }

        public Metrics(String filename, String name)
            : this(filename, name, 0)
        {
        }

        public Metrics()
            : this(String.Empty, String.Empty)
        {
        }

        public Metrics(Metrics other)
            : this(other.Filename, other.Name, other.Position)
        {
            LINES = other.LINES;
            LLOC = other.LLOC;
            LLOCi = other.LLOCi;
            LLOW = other.LLOW;
            PROCS = other.PROCS;
            CARGS = other.CARGS;
            CC = other.CC;
            DC = other.DC;
        }

        //! Clear the metrics
        public void Clear()
        {
            Filename = String.Empty;
            Name = String.Empty;
            Position = 0;
            LINES = 0;
            LLOC = 0;
            LLOCi = 0;
            LLOW = 0;
            PROCS = 0;
            CARGS = 0;
            CC = 0;
            DC = 0;
        }

        /// <summary>
        /// Name of the file where this item comes from
        /// </summary>
        public String Filename
        { get; set; }
	
        /// <summary>
        /// Name of the item
        /// </summary>
        public String Name
        { get; set; }

        /// <summary>
        /// Position of the item in the file (line number)
        /// </summary>
        public Int32 Position
        { get; private set; }

        public void IncrementLINES(Int32 n)         { LINES += n; }
        public void IncrementLLOC(Int32 n)          { LLOC += n; }
        public void IncrementLLOCi(Int32 n)         { LLOCi += n; }
        public void IncrementLLOW(Int32 n)          { LLOW += n; }
        public void IncrementCC(Int32 n)            { CC += n; }
        public void IncrementDC(Int32 n)            { DC += n; }
        public void IncrementPROCS(Int32 n)         { PROCS += n; }
        public void IncrementCARGS(Int32 n)         { CARGS += n; }

        //! Count of logical lines of code
        public Int32 LLOC { get; private set; }

        //! Count of logical lines of comments
        public Int32 LLOCi { get; private set; }

        //! Count of logical lines of whitespaces
        public Int32 LLOW { get; private set; }

        /// <summary>
        /// Sum of all logical lines
        /// </summary>
        public Int32 LLINES     { get { return LLOW+LLOCi+LLOC; } }

        //! Count of total lines
        public Int32 LINES { get; private set; }

        //! Cyclomatic complexity (McCabe)
        public Int32 CC { get; private set; }

        //! Cyclomatic complexity (Depth complexity)
        public Int32 DC { get; private set; }

        //! Number of procedures
        public Int32 PROCS { get; private set; }
        
        //! Number of arguments
        public Int32 CARGS { get; private set; }

        //! CC / PROCS
        public Double CC_PROCS
        {
            get
            {
                if (PROCS > 0)
                    return (double)CC / (double)PROCS;
                return CC;
            }
        }

        //! DC / PROCS
        public Double DC_PROCS
        {
            get
            {
                if (PROCS > 0)
                    return (double)DC / (double)PROCS;
                return DC;
            }
        }

        /// <summary>
        /// Operator + for metrics
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Metrics operator + (Metrics m1, Metrics m2)
        {
            Metrics m = new Metrics(m1);
            m.LINES += m2.LINES;
            m.LLOC += m2.LLOC;
            m.LLOCi += m2.LLOCi;
            m.LLOW += m2.LLOW;
            m.PROCS += m2.PROCS;
            m.CARGS += m2.CARGS;
            m.CC += m2.CC;
            m.DC += m2.DC;
            return m;
        }

        /// <summary>
        /// Operator / for metrics
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Metrics operator /(Metrics m2, Int32 count)
        {
            Metrics m = new Metrics(m2);
            if (count > 0)
            {
                m.LINES /= count;
                m.LLOC /= count;
                m.LLOCi /= count;
                m.LLOW /= count;
                m.PROCS /= count;
                m.CARGS /= count;
                m.CC /= count;
                m.DC /= count;
            }
            return m;
        }
        
        public override string ToString()
        {
            return String.Format("{0} #{1} @{2}", Name, CARGS, Position);
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            Name = reader.ReadElementString("Name");
            Position = Int32.Parse(reader.ReadElementString("Position"));

            CARGS = Int32.Parse(reader.ReadElementString("CARGS"));
            LINES = Int32.Parse(reader.ReadElementString("LINES"));
            LLOC = Int32.Parse(reader.ReadElementString("LLOC"));
            LLOCi = Int32.Parse(reader.ReadElementString("LLOCi"));
            LLOW = Int32.Parse(reader.ReadElementString("LLOW"));
            CC = Int32.Parse(reader.ReadElementString("CC"));
            DC = Int32.Parse(reader.ReadElementString("DC"));
            PROCS = Int32.Parse(reader.ReadElementString("PROCS"));
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("Position", Position.ToString());

            writer.WriteElementString("CARGS", CARGS.ToString());
            writer.WriteElementString("LINES", LINES.ToString());
            writer.WriteElementString("LLOC", LLOC.ToString());
            writer.WriteElementString("LLOCi", LLOCi.ToString());
            writer.WriteElementString("LLOW", LLOW.ToString());
            writer.WriteElementString("CC", CC.ToString());
            writer.WriteElementString("DC", DC.ToString());
            writer.WriteElementString("PROCS", PROCS.ToString());
        }

        #endregion

		public float GetValue (string name)
		{
			switch (name)
			{
			case "CC": return (float)CC;
			case "DC": return (float)DC;
			case "LLOC": return (float)LLOC;
			case "LLOCi": return (float)LLOCi;
			case "CARGS": return (float)CARGS;
			}
			throw new NotImplementedException ();
		}
	}
}
