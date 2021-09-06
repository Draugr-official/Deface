using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Objects
{
    struct DataType
    {
        public DataTypes Type { get; set; }

        /// <summary>
        /// Determines datatype of value
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="fromName">Wether datatype should be found by identifier or value</param>
        public DataType(string Value, bool fromName)
        {
            this.Type = DataTypes.Object;

            if(fromName)
            {
                this.Type = (DataTypes)Enum.Parse(typeof(DataTypes), Value.First().ToString().ToUpper() + Value.Substring(1));
            }
            else
            {
                if(bool.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Bool;
                }
                else if(byte.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Byte;
                }
                else if (short.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Short;
                }
                else if (ushort.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Ushort;
                }
                else if (int.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Int;
                }
                else if (uint.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Uint;
                }
                else if (long.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Long;
                }
                else if (ulong.TryParse(Value, out _))
                {
                    this.Type = DataTypes.Ulong;
                }
                else
                {
                    this.Type = DataTypes.String;
                }
            }
        }

        /// <summary>
        /// Determines wether a keyword is a datatype
        /// </summary>
        /// <returns></returns>
        public static bool IsDataType(string Keyword)
        {
            return Enum.IsDefined(typeof(DataTypes), Keyword.First().ToString().ToUpper() + Keyword.Substring(1));
        }
    }

    public enum DataTypes
    {
        Void,
        String,
        Bool,
        Byte,
        Sbyte,
        Short,
        Ushort,
        Int,
        Uint,
        Long,
        Ulong,
        Float,
        Decimal,
        Double,
        Object,
    }
}
