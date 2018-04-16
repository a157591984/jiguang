using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 数形数据节点实体类
	/// </summary>
	public class TreeNodeInfo {

		/// <summary>
		/// 节点ID
		/// </summary>
		public string NodeID { get; set; }

		/// <summary>
		/// 当前节点的上级节点ID
		/// </summary>
		public string ParentNodeID { get; set; }

		/// <summary>
		/// 当前节点的名称
		/// </summary>
		public string NodeName { get; set; }

		/// <summary>
		/// 节点的优先级
		/// </summary>
		public int Priority { get; set; }
	}
}
