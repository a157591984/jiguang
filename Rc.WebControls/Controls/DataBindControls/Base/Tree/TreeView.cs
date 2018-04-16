using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Rc.WebControls {

	/// <summary>
	/// 自定义的TreeView
	/// </summary>
	public class TreeView : System.Web.UI.WebControls.TreeView, IDataBindControls {

		#region 属性定义

		/// <summary>
		/// 是否已经添加过项目，因为可能用户会强制刷新控件，这个开关防止重复添加。
		/// </summary>
		private const string IS_ADDED_LIST_ITEM = "is_added_list_item";

		private IList<TreeNodeInfo> _infoes = new List<TreeNodeInfo>();
		private IList<TreeNode> _addedNodes = new List<TreeNode>();		//已经添加的树节点

		/// <summary>
		/// 设置或获取当前数据源的集合
		/// </summary>
		public IList<TreeNodeInfo> ItemsSources {
			get { return _infoes; }
			set { _infoes = value; }
		}

		/// <summary>
		/// 在树节点被点击时被触发的脚本格式字符串
		/// </summary>
		public string JavaScriptForm { get; set; }

		/// <summary>
		/// 过滤的节点ID，如果设置了，只有本节点的下级几点才会显示
		/// </summary>
		public string FielterNodeValue { get; set; }

		#endregion

		#region 重写方法
		/// <summary>
		/// OnPreRender
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {
			if (this.ViewState[IS_ADDED_LIST_ITEM] == null) {
				AddNodeItem();
			}			
			base.OnPreRender(e);
		}

		#endregion

		#region 添加树节点

		/// <summary>
		/// 添加树节点
		/// </summary>
		protected virtual void AddNodeItem() {
			var parentIds = _infoes.Select(p => p.ParentNodeID);
			var infoes = _infoes.Where(p => !parentIds.Contains(p.NodeID)).ToList();			//获取叶子节点

			for (int i = 0; i < infoes.Count; i++) {
				bool isFindFielterNodeValue = false;
				var currentNode = new TreeNode(infoes[i].NodeName, infoes[i].NodeID);
				if (!string.IsNullOrEmpty(JavaScriptForm)) {
					currentNode.NavigateUrl = string.Format(JavaScriptForm, infoes[i].NodeID);
				}

				var treeNode = GetModeParentPath(infoes[i], currentNode, ref isFindFielterNodeValue);
				//如果设置了按节点过滤，那么如果不包含，那么就移除节点
				if (string.IsNullOrWhiteSpace(FielterNodeValue) || isFindFielterNodeValue) {

					bool isFind = false;
					foreach (TreeNode node in this.Nodes) {
						if (node.Value == treeNode.Value) {
							isFind = true;
							break;
						}
					}

					if (!isFind) {
						this.Nodes.Add(treeNode);
					}
				}
			}
			this.CollapseAll();
			this.ViewState[IS_ADDED_LIST_ITEM] = "true";
		}

		private TreeNode GetModeParentPath(TreeNodeInfo treeInfo, TreeNode treeNode, ref bool isFindFielterNodeValue) {

			IList<TreeNodeInfo> infoes = _infoes.Where(p => p.NodeID == treeInfo.ParentNodeID).ToList();
			if (infoes.Count() == 0) {
				return treeNode;
			}

			IList<TreeNode> parentNodes = _addedNodes.Where(p => p.Value == infoes[0].NodeID).ToList();
			TreeNode parentNode;
			if (parentNodes.Count() == 0) {
				parentNode = new TreeNode(infoes[0].NodeName, infoes[0].NodeID);
				if (!string.IsNullOrEmpty(JavaScriptForm)) {
					parentNode.NavigateUrl = string.Format(JavaScriptForm, infoes[0].NodeID);
				}
				_addedNodes.Add(parentNode);
			}
			else {
				parentNode = parentNodes[0];
			}

			if (!isFindFielterNodeValue) {
				isFindFielterNodeValue = (infoes[0].NodeID == FielterNodeValue);
				if (isFindFielterNodeValue) {			//如果找到限制节点，那么停止递归
					return treeNode;
				}
			}


			bool isFind = false;
			foreach (TreeNode node in parentNode.ChildNodes) {
				if (node.Value == treeNode.Value) {
					isFind = true;
					break;
				}
			}
			if (!isFind) {
				parentNode.ChildNodes.Add(treeNode);
			}

			return GetModeParentPath(infoes[0], parentNode, ref isFindFielterNodeValue);
		}

		#endregion

		#region 刷新列表项

		/// <summary>
		/// 重新加载列表项
		/// </summary>
		public void ReLoadItem() {
			this.Nodes.Clear();
			this.ViewState[IS_ADDED_LIST_ITEM] = null;
			AddNodeItem();
		}

		#endregion

		#region 获取树的所有节点

		/// <summary>
		/// 获取树的所有节点
		/// </summary>
		/// <returns></returns>
		public IList<TreeNode> GetAllNodes() {
			IList<TreeNode> nodes = new List<TreeNode>();
			foreach (TreeNode rootNode in this.Nodes) {
				nodes.Add(rootNode);
				GetSubNodes(rootNode, ref nodes);
			}

			return nodes;
		}

		private void GetSubNodes(TreeNode node, ref IList<TreeNode> nodes) {
			var subNodes = node.ChildNodes;
			foreach (TreeNode subNode in subNodes) {
				nodes.Add(subNode);
				GetSubNodes(subNode, ref nodes);
			}
		}

		#endregion
	}
}