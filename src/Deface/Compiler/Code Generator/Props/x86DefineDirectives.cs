using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props
{
	/// <summary>
	/// x86 define directives for initialized data
	/// </summary>
	public enum x86DefineDirectives
    {
		/// <summary>
		/// x86 'define byte', 1 byte
		/// </summary>
		db,

		/// <summary>
		/// x86 'define word', 2 bytes
		/// </summary>
		dw,

		/// <summary>
		/// x86 'define dword', 4 bytes
		/// </summary>
		dd,

		/// <summary>
		/// x86 'define qword', 8 bytes
		/// </summary>
		dq,

		/// <summary>
		/// x86 'define ten bytes', 10 bytes
		/// </summary>
		dt,

		/// <summary>
		/// x86 'equate'
		/// </summary>
		equ,
	}
}
